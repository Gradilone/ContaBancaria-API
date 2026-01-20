# ContaBancaria API

Esta é uma API REST robusta desenvolvida para simular um sistema bancário digital. O projeto foca em segurança, integridade de dados financeiros e boas práticas de arquitetura.

## Funcionalidades Principais

* **Sistema de Usuários:** Cadastro e Autenticação via JWT.
* **Operações Bancárias:**
    * Consulta de Saldo em tempo real.
    * Extrato detalhado de transações.
    * Transferências entre contas com validação de saldo.
* **Segurança:** Proteção de endpoints sensíveis através de Claims e Tokens.

## Tecnologias e Ferramentas

* **.NET 8.0** (C#)
* **Entity Framework Core** (Migrations e Mapeamento ORM)
* **SQL Server** (Persistência de dados)
* **JWT (JSON Web Token)** (Autenticação e Autorização)
* **Swagger/OpenAPI** (Documentação e testes da API)
* **Repository Pattern** (Padronização do acesso a dados)
---
## Configuração e Execução

### 1. Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* SQL Server (LocalDB ou Express)

### 2. Clonar o Projeto
```bash
git clone [https://github.com/SeuUsuario/ContaBancaria-API.git](https://github.com/SeuUsuario/ContaBancaria-API.git)
cd ContaBancaria-API
