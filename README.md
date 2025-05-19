# CP2- C#

## 📌 Descrição do Projeto

Este projeto consiste em uma API RESTful desenvolvida com ASP.NET Core, com objetivo de gerenciar entidades como Motos, Filiais e Pátios. A aplicação implementa operações básicas de CRUD (Create, Read, Update, Delete) e segue uma arquitetura em camadas (Controllers, Application, Domain, Infrastructure). Tem o objetivo de representar as relações entre as Filiais, Pátios e Motos.

## 👥 Nome e RM dos Integrantes

- Guilherme Camasmie Laiber de Jesus – RM554894

- Fernando Fernandes Prado – RM557982

- Pedro Manzo Yokoo – RM556115

## 🚀 Rotas Disponíveis

### 📍 MotoController - `/api/Moto`
- `GET /api/Moto`  
  Retorna todas as motos cadastradas.

- `GET /api/Moto/placa`  
  Retorna uma moto específica pela placa.

- `POST /api/Moto`  
  Cria uma nova moto. Requer um corpo com os dados da moto.

- `PUT /api/Moto/placa`  
  Atualiza os dados de uma moto pela placa.

- `DELETE /api/Moto/placa`  
  Deleta os dados de uma moto pela placa.

> Os outros controllers (`FilialController`, `PatioController`) seguem estrutura semelhante com operações básicas de CRUD.

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

## ▶️ Instruções de Execução

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/Gui11epio/CP2_C-.git
   

2. **Vá até "lauchSettings.json"**
   
   ![image](https://github.com/user-attachments/assets/adaf4e75-7381-4550-9252-163149c1f16c)

3. **Coloque suas informações do Banco de Dados Oracle**

   ![image](https://github.com/user-attachments/assets/70c5914a-b683-406a-ac77-849e88a52bc9)

4. **Rode o programa e o Swagger abrirá sozinho**


## 📬JSON de Teste

- Filial:
  
```bash
{
  "nome": "Filial Centro-SP",
  "cidade": "São Paulo",
  "pais": "Brasil"
}
```

- Pátio:
  
```bash
{
  "nome": "Pátio A1",
  "largura": "50.0",
  "comprimento": "120.0",
  "filialId": 1
}
```
ℹ️ Observação: largura e comprimento devem ser strings representando valores numéricos válidos (entre 5 e 500 para largura; entre 5 e 1000 para comprimento).


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

  



   
