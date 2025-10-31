#!/bin/bash
set -e  # Encerra o script se ocorrer qualquer erro

# ==============================
# VARIÁVEIS
# ==============================
RG="RG_Mottu"
LOCATION="eastus"
RANDOM_ID=$((RANDOM % 9000 + 1000))
ACR_NAME="motturegistry$RANDOM_ID"
ACR_LOGIN="$ACR_NAME.azurecr.io"
MYSQL_ACI_NAME="mottu-mysql"
API_ACI_NAME="mottu-api"
IMAGE_TAG="$ACR_LOGIN/mottuapi:v1"
MYSQL_ROOT_PWD="SenhaForte123!"
MYSQL_DB="mottu"

# ==============================
# RESOURCE GROUP
# ==============================

echo "🔹 Criando Resource Group..."
az group create --name "$RG" --location "$LOCATION"

# ==============================
# CRIAR ACR
# ==============================
echo "🔹 Criando Azure Container Registry ($ACR_NAME)..."
az acr create --resource-group "$RG" --name "$ACR_NAME" --sku Basic --admin-enabled true
az acr login --name "$ACR_NAME"

# Obter credenciais do ACR
echo "🔹 Obtendo credenciais do ACR..."
acr_user=$(az acr credential show --name "$ACR_NAME" --query "username" -o tsv)
acr_pass=$(az acr credential show --name "$ACR_NAME" --query "passwords[0].value" -o tsv)

# ==============================
# IMPORTAR MYSQL PARA O ACR
# ==============================
echo "🔹 Importando imagem MySQL para o ACR..."
az acr import --name "$ACR_NAME" --source docker.io/library/mysql:8.0 --image mysql:8.0

# ==============================
# CRIAR CONTAINER MYSQL
# ==============================
MYSQL_DNS="$MYSQL_ACI_NAME$((RANDOM % 9000 + 1000))"

echo "🔹 Criando container MySQL..."
az container create \
  --resource-group "$RG" \
  --name "$MYSQL_ACI_NAME" \
  --image "$ACR_LOGIN/mysql:8.0" \
  --registry-login-server "$ACR_LOGIN" \
  --registry-username "$acr_user" \
  --registry-password "$acr_pass" \
  --cpu 1 --memory 2 \
  --ports 3306 \
  --environment-variables MYSQL_ROOT_PASSWORD="$MYSQL_ROOT_PWD" MYSQL_DATABASE="$MYSQL_DB" \
  --dns-name-label "$MYSQL_DNS" \
  --location "$LOCATION" \
  --os-type Linux

echo "⏳ Aguardando inicialização do MySQL (60s)..."
sleep 60

# ==============================
# OBTER FQDN DO MYSQL
# ==============================
mysql_fqdn=$(az container show --resource-group "$RG" --name "$MYSQL_ACI_NAME" --query "ipAddress.fqdn" -o tsv)
echo "✅ MySQL FQDN: $mysql_fqdn"

# ==============================
# CONFIGURAR CONNECTION STRING E RODAR MIGRATIONS
# ==============================
echo "🔹 Configurando connection string e executando migrations..."

cd ./MottuFind_Cloud/MottuFind_C_.Infrastructure || exit

export DEFAULT_CONNECTION="server=$mysql_fqdn;port=3306;database=$MYSQL_DB;user=root;password=$MYSQL_ROOT_PWD"

dotnet ef database update

# ==============================
# BUILD E PUSH DA IMAGEM DA API
# ==============================
cd ../../MottuFind_Cloud || exit

echo "🔹 Buildando e enviando imagem da API..."
docker build -t "$IMAGE_TAG" -f MottuFind/Dockerfile .
docker push "$IMAGE_TAG"

# ==============================
# CRIAR CONTAINER DA API
# ==============================
API_DNS="$API_ACI_NAME$((RANDOM % 9000 + 1000))"

echo "🔹 Criando container da API..."
az container create \
  --resource-group "$RG" \
  --name "$API_ACI_NAME" \
  --image "$IMAGE_TAG" \
  --registry-login-server "$ACR_LOGIN" \
  --registry-username "$acr_user" \
  --registry-password "$acr_pass" \
  --ports 8080 \
  --environment-variables DEFAULT_CONNECTION="$DEFAULT_CONNECTION" \
  --cpu 1 --memory 1 \
  --dns-name-label "$API_DNS" \
  --location "$LOCATION" \
  --os-type Linux

echo "⏳ Aguardando inicialização da API (30s)..."
sleep 30

# ==============================
# EXIBIR URLs
# ==============================
api_fqdn=$(az container show --resource-group "$RG" --name "$API_ACI_NAME" --query "ipAddress.fqdn" -o tsv)
echo "=============================================="
echo "✅ Deploy concluído!"
echo "🌐 API URL: http://$api_fqdn:8080/swagger/index.html"
echo "🗄️  MySQL Host: $mysql_fqdn"
