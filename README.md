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
## Configuração e Execução (Docker)

Projeto configurado com docker!

### 1. Pré-requisitos
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) instalado e rodando.

### 2. Clonar e Rodar
No seu terminal, execute os comandos abaixo:

```bash
# Clone o repositório
git clone [https://github.com/SeuUsuario/ContaBancaria-API.git](https://github.com/SeuUsuario/ContaBancaria-API.git)

# Entre na pasta
cd ContaBancaria-API

# Suba a aplicação e o banco de dados
docker-compose up --build

```

## Fluxo de Teste Sugerido

Para validar o funcionamento completo da API , siga estes passos através da interface do Swagger:

1.  **Criar Conta:**
    * Vá ao endpoint `POST /api/User/register`.
    * Envie os dados do novo usuário. Se houver sucesso, os dados serão gravados no SQL Server via container.

2.  **Autenticar (Login):**
    * Vá ao endpoint `POST /api/User/login`.
    * Informe as credenciais criadas. O sistema retornará um **Token JWT**. Copie este token.

3.  **Autorizar no Swagger:**
    * Role até o topo da página e clique no botão **Authorize** (ícone de cadeado).
    * No campo *Value*, digite: `Bearer SEU_TOKEN_AQUI` (substituindo pela chave que você copiou).
    * Clique em **Authorize** e depois em **Close**.

4.  **Consultar Saldo:**
    * Vá ao endpoint `GET /api/Account/balance`.
    * Clique em *Try it out* e depois em *Execute*.
    * Se tudo estiver correto, você receberá um `Status 200` com o saldo inicial de **100**.

4.  **Consultar contas: (endpoint para facilitar testes)**
    * Vá ao endpoint `GET /api/Account/listAccounts`.
    * Clique em *Try it out* e depois em *Execute*.
    * Aqui você receberá a lista de todos os usuários cadastrados e seus respectivos dados como o número da conta para realizar transferências

5.  **Transferir: **
    * Vá ao endpoint `GET /transfer`.
    * Clique em *Try it out* e depois em *Execute*.
    * Preencha o número da conta a ser transferida e o valor nos campos indicados
    * Se tudo estiver correto, você receberá um `Status 200` com mensagem de confirmação e o valor trasnferido será debitado da conta atual e adicionado a conta desejada.

6.  **Consultar extrato: **
    * Vá ao endpoint `GET /api/Account/extract`.
    * Clique em *Try it out* e depois em *Execute*.
    * Aqui você receberá a lista de todas as movimentações feitas na conta atual logada

