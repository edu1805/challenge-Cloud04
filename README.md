# 🛵 MotoMap

Este projeto foi desenvolvido como parte do desafio da faculdade, com o objetivo de criar uma **API REST** que representa digitalmente um pátio de motos da empresa **Mottu**, exibindo a localização e status de cada moto em tempo real, utilizando sensores RFID e tecnologias modernas de backend.

### Integrantes
- **Eduardo do Nascimento Barriviera - RM555309**
- **Thiago Lima de Freitas - RM556795**
- **Bruno Centurion Fernandes - RM556531**

---

## 🚀 Funcionalidades

- ✅ Cadastro de motos com validações de negócio
- ✅ Atualização de posição e status em tempo real
- ✅ **Autenticação JWT** com hash de senha (HMACSHA512)
- ✅ **Versionamento de API** (V1 e V2)
- ✅ **Health Check** para monitoramento da aplicação
- ✅ API RESTful com endpoints organizados
- ✅ Documentação Swagger integrada
- ✅ Acesso e persistência de dados em banco Oracle via EF Core
- ✅ **Testes unitários** com xUnit

---

## 🏗 Estrutura da Aplicação

A aplicação segue uma arquitetura **em camadas**, garantindo manutenção e escalabilidade:

- **Domain:** Contém entidades e regras de negócio centrais
- **Application:** Camada de casos de uso, DTOs, validações e lógica de aplicação
- **Infrastructure:** Implementação de acesso a dados, integração com API e recursos externos
- **API:** Camada que expõe os endpoints REST consumidos pelo frontend
- **Tests:** Testes unitários com xUnit

---

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- **ASP.NET Core Web API**
- **Entity Framework Core** + `Oracle.EntityFrameworkCore`
- **AutoMapper** para mapeamento entre entidades e DTOs
- **FluentValidation** para validação de DTOs
- **Swagger / Swashbuckle** para documentação da API
- **JWT (JSON Web Token)** para autenticação
- **Health Checks** para monitoramento
- **xUnit** para testes unitários
- **Oracle Database** como banco de dados relacional

---

## 🔐 Autenticação

A API utiliza **JWT (JSON Web Token)** para autenticação. As senhas são armazenadas com hash **HMACSHA512** com salt único por usuário.

### Como autenticar:

1. **Criar usuário:** `POST /api/v2/usuario`
2. **Fazer login:** `POST /api/v2/login`
3. **Usar o token:** Adicionar no header `Authorization: Bearer {seu_token}`

---

## 📊 Health Check

A aplicação possui endpoints de monitoramento:

- **Health Check simples:** `GET /health`
- **Health Check detalhado:** `GET /health-details`

Retorna informações sobre:
- Status da aplicação
- Conectividade com banco de dados
- Outros recursos críticos

---

## 📌 Exemplos dos Endpoints

### 🔐 Autenticação (V2)

#### Criar usuário
**POST** `http://localhost:5273/api/v2/usuario`
```json
{
  "nome": "João Silva",
  "email": "joao@email.com",
  "senha": "senha123",
  "motoId": null
}
```

#### Login
**POST** `http://localhost:5273/api/v2/login`
```json
{
  "email": "joao@email.com",
  "senha": "senha123"
}
```

**Retorno:**
```json
{
  "sucesso": true,
  "mensagem": "Login realizado com sucesso",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "usuarioId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "nome": "João Silva",
  "email": "joao@email.com"
}
```

---

### 🏍️ Motos (V1 - Requer autenticação)

#### 1️⃣ Listar todas as motos
**GET** `http://localhost:5273/api/v1/moto`

**Retorno esperado:**
```json
[
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "placa": "ABC1D23",
    "posicao": "A1",
    "status": "pronta",
    "ultimaAtualizacao": "2025-10-01T22:31:36.342Z"
  }
]
```

#### 2️⃣ Cadastrar uma nova moto
**POST** `http://localhost:5273/api/v1/moto/criar`

**Headers:**
```
Authorization: Bearer {seu_token}
Content-Type: application/json
```

**Corpo da requisição:**
```json
{
  "placa": "ABC1D23",
  "posicao": "A1",
  "status": "pronta",
  "ultimaAtualizacao": "2025-10-01T22:31:36.342Z"
}
```

**Status permitidos:** `"pronta"`, `"revisao"`, `"reservada"`, `"fora de serviço"`

#### 3️⃣ Atualizar posição ou status de uma moto
**PUT** `http://localhost:5273/api/v1/moto/editar/{id}`

**Headers:**
```
Authorization: Bearer {seu_token}
```

**Corpo da requisição:**
```json
{
  "posicao": "B3",
  "status": "revisao"
}
```

#### 4️⃣ Deletar uma moto
**DELETE** `http://localhost:5273/api/v1/moto/deletar/{id}`

**Headers:**
```
Authorization: Bearer {seu_token}
```

**Retorno esperado:** `204 No Content`

---

## ⚙️ Como Rodar o Projeto

### ✅ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Banco de dados Oracle acessível
- Visual Studio 2022 ou superior (recomendado) ou Rider

---

### 📦 1. Clonar o repositório
```bash
git clone https://github.com/edu1805/Challange-DotNet04.git
cd Challange-DotNet04
```

---

### 🔧 2. Configurar o banco de dados Oracle

No arquivo `appsettings.json`, configure a sua string de conexão:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=oracle.fiap.com.br:1521/orcl;User ID=SEU_ID;Password=SUA_PASSWORD"
  },
  "Jwt": {
    "SecretKey": "SUA_CHAVE_SECRETA_AQUI_COM_PELO_MENOS_32_CARACTERES"
  }
}
```

---

### 🧱 3. Gerar as Migrations (Se necessário)
```bash
dotnet tool install --global dotnet-ef

# Criar migration inicial
dotnet ef migrations add Inicial -p ChallengeMottu.Infrastructure -s ChallengeMottu.Api

# Aplicar no banco
dotnet ef database update -p ChallengeMottu.Infrastructure -s ChallengeMottu.Api
```

---

### ▶️ 4. Executar a aplicação
```bash
dotnet run --project ChallengeMottu.Api
```

Ou direto pelo Visual Studio com **F5**.

---

### 📖 Acessar a documentação Swagger
```
https://localhost:{port}/swagger
```

---

## 🧪 Testes Unitários

O projeto possui testes unitários implementados com **xUnit**.

### 📁 Estrutura de testes
```
ChallengeMottu.Tests/
├── UsuarioServiceTests.cs
├── MotoServiceTests.cs
│   
└── ...
```

### ▶️ Como rodar os testes

#### Rodar todos os testes:
```bash
dotnet test
```

#### Rodar testes com detalhamento:
```bash
dotnet test --logger "console;verbosity=detailed"
```

#### Rodar testes de um projeto específico:
```bash
dotnet test ChallengeMottu.Tests/ChallengeMottu.Tests.csproj
```

#### Rodar testes com cobertura de código:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### 📊 Visualizar resultados

Os resultados dos testes aparecem no console com indicação de:
- ✅ Testes passados
- ❌ Testes falhos
- ⏭️ Testes ignorados
- ⏱️ Tempo de execução

---

## 📚 Versionamento da API

A API possui duas versões:

### **V1** - Versão estável
- Endpoints de Motos
- Endpoints de Localização

### **V2** - Nova versão com autenticação
- Endpoints de Autenticação (Login/Registro)
- Endpoints de Usuários

**Exemplo de acesso:**
- V1: `https://localhost:port/api/v1/moto`
- V2: `https://localhost:port/api/v2/login`

---

## 📋 Status Codes

| Código | Descrição |
|--------|-----------|
| 200 | OK - Requisição bem-sucedida |
| 201 | Created - Recurso criado com sucesso |
| 204 | No Content - Recurso deletado |
| 400 | Bad Request - Dados inválidos |
| 401 | Unauthorized - Token ausente/inválido |
| 404 | Not Found - Recurso não encontrado |
| 500 | Internal Server Error - Erro no servidor |

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais como parte do desafio da FIAP.

Teste de pipeline
