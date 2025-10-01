# Sprint 3

### 👥 Nome e RM dos Integrantes

- Guilherme Camasmie Laiber de Jesus – RM554894

- Fernando Fernandes Prado – RM557982

- Pedro Manzo Yokoo – RM556115

### 📌 Descrição do Projeto

Este projeto consiste em uma API RESTful desenvolvida com ASP.NET Core, tenta representar uma solução de monitoramento de motos por meio de Rfid, com objetivo de gerenciar entidades como Motos, Filiais, Pátios, Usuários, LeitorRFID e LeituraRFID. 

A TagRfid é criada automaticamente quando uma moto é cadastrada.

### 📌 Arquitetura do Projeto

A aplicação implementa operações básicas de CRUD (Create, Read, Update, Delete), segue uma arquitetura em camadas (Controllers, Application, Domain, Infrastructure), segue os príncipios de DDD e Clean Code.

Com o objetivo de deixar a aplicação mais organizada e destribuir as responsabilidades

## 🚀 Rotas Disponíveis

### 📍 MotoController - `/api/Moto`
- `GET /api/Moto`  
  Retorna todas as motos cadastradas.

- `GET /api/Moto/placa`  
  Retorna uma moto específica pela placa.

- `GET /api/Moto/pagina`  
  Retorna motos por meio de páginas.

- `POST /api/Moto`  
  Cria uma nova moto. Requer um corpo com os dados da moto.

- `PUT /api/Moto/placa`  
  Atualiza os dados de uma moto pela placa.

- `DELETE /api/Moto/placa`  
  Deleta os dados de uma moto pela placa.

> Os outros controllers (`FilialController`, `PatioController`, `UsuarioController`, `LeitorRFIDController` e `LeituraRFIDController`) seguem estrutura semelhante com operações básicas de CRUD.

## 🛠️ Tecnologias Utilizadas

- [.NET 6 / ASP.NET Core](https://dotnet.microsoft.com/)
- C#
- Entity Framework Core
- Swagger (OpenAPI) para documentação
- Visual Studio 2022
- Oracle DataBase
- AutoMapper
- Migrations
- DataAnnotations
- Pagination
- HATEOAS

## ▶️ Instruções de Execução

1. **Clone o repositório:**
   ```bash
   https://github.com/Gui11epio/MottuFind_Cloud.git
   ```
   
2. **Variáveis**
````bash
$RG = "RG_Mottu"
$LOCATION = "eastus"                     
$ACR_NAME = "motturegistry$((Get-Random -Minimum 1000 -Maximum 9999))"
$ACR_LOGIN = "$ACR_NAME.azurecr.io"
$MYSQL_ACI_NAME = "mottu-mysql"
$API_ACI_NAME = "mottu-api"
$IMAGE_TAG = "$ACR_LOGIN/mottuapi:v1"
$MYSQL_ROOT_PWD = "SenhaForte123!"
$MYSQL_DB = "mottu"
````

3. **Login e resource group**
````bash
az login
az group create --name $RG --location $LOCATION
````

4. **Criar ACR**
````bash
az acr create --resource-group $RG --name $ACR_NAME --sku Basic --admin-enabled true
az acr login --name $ACR_NAME
````

5. **Obter credenciais ACR (para uso posterior)**
````bash
$acr_user = (az acr credential show --name $ACR_NAME --query "username" -o tsv)
$acr_pass = (az acr credential show --name $ACR_NAME --query "passwords[0].value" -o tsv)
````

6. **Criar MySQL no ACI**
````bash
// importar a imagem Mysql para o acr
az acr import --name $ACR_NAME --source docker.io/library/mysql:8.0 --image mysql:8.0


az container create `
  --resource-group $RG `
  --name $MYSQL_ACI_NAME `
  --image "$ACR_LOGIN/mysql:8.0" `
  --registry-login-server $ACR_LOGIN `
  --registry-username $acr_user `
  --registry-password $acr_pass `
  --cpu 1 --memory 2 `
  --ports 3306 `
  --environment-variables "MYSQL_ROOT_PASSWORD=$MYSQL_ROOT_PWD" "MYSQL_DATABASE=$MYSQL_DB" `
  --dns-name-label "$MYSQL_ACI_NAME$((Get-Random -Minimum 1000 -Maximum 9999))" `
  --location $LOCATION `
  --os-type Linux

# Aguarde o MySQL inicializar
Start-Sleep -Seconds 60
````

7. **Obter FQDN do MySQL**
````bash
$mysql_fqdn = az container show --resource-group $RG --name $MYSQL_ACI_NAME --query "ipAddress.fqdn" -o tsv
Write-Host "MySQL FQDN: $mysql_fqdn"
````


8. **Configurar connection string e rodar migrations**
````bash
cd .\MottuFind_Cloud\

cd .\MottuFind_C_.Infrastructure\

$env:DEFAULT_CONNECTION = "server=$mysql_fqdn;port=3306;database=$MYSQL_DB;user=root;password=$MYSQL_ROOT_PWD"

# Navegar para a pasta do projeto e executar migrations

dotnet ef database update
````

9. **Build e push da imagem da API**
````bash
cd ..\MottuFind_Cloud\
docker build -t $IMAGE_TAG -f MottuFind/Dockerfile .
docker push $IMAGE_TAG
````

10. **Criar container da API**
````bash
$DEFAULT_CONNECTION = "server=$mysql_fqdn;port=3306;database=$MYSQL_DB;user=root;password=$MYSQL_ROOT_PWD"

az container create `
  --resource-group $RG `
  --name $API_ACI_NAME `
  --image $IMAGE_TAG `
  --registry-login-server $ACR_LOGIN `
  --registry-username $acr_user `
  --registry-password $acr_pass `
  --ports 8080 `
  --environment-variables "DEFAULT_CONNECTION=$DEFAULT_CONNECTION" `
  --cpu 1 --memory 1 `
  --dns-name-label "$API_ACI_NAME$((Get-Random -Minimum 1000 -Maximum 9999))" `
  --location $LOCATION `
  --os-type Linux
````

11. **Testar API**
````bash
Start-Sleep -Seconds 30

$api_fqdn = az container show --resource-group $RG --name $API_ACI_NAME --query "ipAddress.fqdn" -o tsv
Write-Host "API URL: http://$api_fqdn:8080/swagger/index.html"
Write-Host "MySQL Host: $mysql_fqdn"


//acessando banco
Host: mottu-mysql7872.eastus.azurecontainer.io 
Porta: 3306 
Usuário: root 
Senha: SenhaForte123! 
Database: mottu
````

## 📬JSON de Teste

- Filial:
  
```bash
{
  "cidade": "São Paulo",
  "pais": "Brasil"
}
```

#

- Pátio:
  
```bash
{
  "nome": "Pátio A1",
  "filialId": 1
}
```

#

- Moto
  
```bash
{
  "placa": "ABC1D23",
  "modelo": "POP",
  "marca": "Yamaha",
  "status": "MANUTENCAO",
  "patioId": 1
}
```
🔤 A placa da Moto deve ser única, não deve repetir

🔤 Modelo e Status devem conter valores válidos dos enums MotoModelo e MotoStatus, como:

- MotoModelo: "POP", "SPORT", "ELETRICA"
  
- MotoStatus: "LIGADO", "DESLIGADO", "MANUTENCAO", "DISPONIVEL"

#

- Usuário
```bash
{
  "setores": "MECANICA",
  "nomeUsuario": "Roberto",
  "email": "roberto@gmail.com",
  "senha": "roB123@!"
}
```
🔤 Setores deve conter:

- Setores: "MECANICA" ou "GARAGEM"


#

- LeitorRFID
```bash
{
  "localizacao": "Portão Principal A",
  "ipDispositivo": "192.168.1.100",
  "patioId": 1
}
```

#

- LeituraRFID
```bash
{
  
  "dataHora": "2025-01-15T14:30:00",
  "leitorId": 1,
  "tagRfidId": 1
}

```

## Estrutura do Projeto


<img width="1536" height="1024" alt="estruturaProjeto" src="https://github.com/user-attachments/assets/012c74b5-a9fa-42d1-b955-6053b83ecf9f" />



  



   
