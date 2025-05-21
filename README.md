# 🎬 FilmesAPI

Uma API RESTful desenvolvida com ASP.NET Core 8.0 para gerenciamento de filmes, utilizando boas práticas de arquitetura, injeção de dependência, DTOs e persistência de dados com MySQL via Entity Framework Core.

## 🚀 Tecnologias Utilizadas

* **.NET 8.0**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **MySQL**
* **AutoMapper**
* **Swashbuckle (Swagger UI)**
* **JsonPatch (atualização parcial de recursos)**

## 📘 Funcionalidades Implementadas

Durante o desenvolvimento desta API, foram aplicados diversos conceitos fundamentais do desenvolvimento backend com ASP.NET Core:

* Estruturação de **Controllers** para expor endpoints RESTful.
* Definição de **rotas e actions** utilizando os verbos HTTP:

  * `POST` para criação de recursos.
  * `GET` para recuperação de dados (com suporte à paginação).
  * `PUT` para atualização completa de registros.
  * `PATCH` para atualização parcial com `JsonPatchDocument`.
  * `DELETE` para exclusão de dados.
* Integração com o **banco de dados MySQL** usando o **Entity Framework Core**.
* Uso de **DTOs (Data Transfer Objects)** para separar modelos de domínio e dados expostos pela API.
* **Injeção de dependência** no construtor dos controllers.
* Paginação via parâmetros `skip` e `take` na URL.
* Documentação da API com **Swagger** para facilitar testes e visualização dos endpoints.
* Configuração de parâmetros sensíveis e de ambiente via `appsettings.json`.

## ▶️ Como Executar o Projeto

### 🔧 Requisitos

* [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* Servidor MySQL (local ou remoto)
* Ferramenta para gerenciamento do banco (como MySQL Workbench)

### 🛠️ Configuração do Banco de Dados

1. Crie um banco de dados no MySQL, por exemplo:

   ```sql
   CREATE DATABASE FilmesDB;
   ```

2. Configure a string de conexão no `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "FilmesConnection": "server=localhost;database=FilmesDB;user=root;password=sua_senha"
   }
   ```

3. Execute as **migrations** (se configuradas):

   ```bash
   dotnet ef database update
   ```

### ▶️ Rodando a API

```bash
cd FilmesAPI
 dotnet restore
 dotnet run
```

Acesse a documentação interativa via Swagger:

```
https://localhost:5001/swagger
```

ou

```
http://localhost:5000/swagger
```

## 📂 Estrutura do Projeto

```
FilmesAPI/
├── Controllers/
│   └── MovieController.cs
├── Models/
│   └── Movie.cs
├── Data/
│   └── MovieContext.cs
├── DTOs/
│   ├── CreateMovieDTOs.cs
│   ├── ReadMovieDTOs.cs
│   └── UpdateMovieDTOs.cs
├── appsettings.json
├── Program.cs
└── FilmesAPI.csproj
```

## 📌 Endpoints Disponíveis

| Método | Rota          | Descrição                             |
| ------ | ------------- | ------------------------------------- |
| POST   | `/movie`      | Cria um novo filme                    |
| GET    | `/movie`      | Lista todos os filmes (com paginação) |
| GET    | `/movie/{id}` | Busca um filme por ID                 |
| PUT    | `/movie/{id}` | Atualiza completamente um filme       |
| PATCH  | `/movie/{id}` | Atualiza parcialmente um filme        |
| DELETE | `/movie/{id}` | Remove um filme                       |

---
