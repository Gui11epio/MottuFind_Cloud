#!/bin/bash

# ================================
# 🚀 DEPLOY MOTTUFIND - AZURE CLI (SQL SERVER)
# ================================

# --- Configurações iniciais ---
RESOURCE_GROUP="rg-mottufind"
LOCATION="brazilsouth"
PLAN_NAME="plan-mottufind"
APP_NAME="mottufind-app"
SQL_SERVER="mottu-sql-server"
SQL_ADMIN="mottuadmin"
SQL_PASSWORD="SenhaForte123!"
SQL_DATABASE="mottudb"

az provider register --namespace Microsoft.Sql

# --- Criar Resource Group ---
az group create --name $RESOURCE_GROUP --location $LOCATION

# --- Criar App Service Plan ---
az appservice plan create --name $PLAN_NAME --resource-group $RESOURCE_GROUP --sku B1

# --- Criar SQL Server ---
az sql server create --name $SQL_SERVER --resource-group $RESOURCE_GROUP --location $LOCATION --admin-user $SQL_ADMIN --admin-password "$SQL_PASSWORD"

# --- Criar Banco de Dados ---
az sql db create --resource-group $RESOURCE_GROUP --server $SQL_SERVER --name $SQL_DATABASE --service-objective S0

# --- Liberar acesso público temporário ---
az sql server firewall-rule create --resource-group $RESOURCE_GROUP --server $SQL_SERVER --name AllowAll --start-ip-address 0.0.0.0 --end-ip-address 255.255.255.255

# --- Criar o App Service ---
az webapp create --resource-group $RESOURCE_GROUP --plan $PLAN_NAME --name $APP_NAME --runtime "dotnet:8" --deployment-local-git

# --- Definir a connection string ---
CONNECTION_STRING="Server=tcp:$SQL_SERVER.database.windows.net,1433;Initial Catalog=$SQL_DATABASE;Persist Security Info=False;User ID=$SQL_ADMIN;Password=$SQL_PASSWORD;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# --- Configurar no App Service somente DefaultConnection ---
az webapp config connection-string set \
  --resource-group $RESOURCE_GROUP \
  --name $APP_NAME \
  --settings "DefaultConnection=$CONNECTION_STRING" \
  --connection-string-type SQLAzure

# --- Aplicar migrações do EF Core localmente ---
# Aqui definimos a variável para o dotnet ef, mas sem afetar o App Service
export DOTNET_CONNECTION_STRING="$CONNECTION_STRING"

# Certifique-se de que o dotnet-ef está instalado
if ! command -v dotnet-ef &> /dev/null
then
    dotnet tool install --global dotnet-ef
    export PATH="$PATH:$HOME/.dotnet/tools"
fi

# Executar as migrações
dotnet ef database update --project ./MottuFind_C_.Infrastructure

echo ""
echo "✅ Deploy concluído com sucesso!"
echo "🌍 Acesse sua aplicação em: https://$APP_NAME.azurewebsites.net"
echo "📜 Logs em tempo real: az webapp log tail --resource-group $RESOURCE_GROUP --name $APP_NAME"
