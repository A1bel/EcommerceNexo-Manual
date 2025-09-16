# EcommerceNexo

Este projeto é um sistema de e-commerce desenvolvido em **ASP.NET Core MVC** com **PostgreSQL** como banco de dados.  
Ele possui duas versões:
- **Versão Manual**: com operações de banco feitas diretamente com comandos SQL.
- **Versão EF**: utilizando o Entity Framework Core.

## 🚀 Tecnologias
- ASP.NET Core MVC
- PostgreSQL
- Entity Framework Core (na versão EF)
- Bootstrap / jQuery

## ⚙️ Como rodar
1. Clone este repositório
2. Configure o banco PostgreSQL
3. Atualize a connection string no `appsettings.json`
4. Rode o projeto pelo Visual Studio ou `dotnet run`

## 📌 Funcionalidades
- Cadastro de usuários
- Login (manual ou com Identity, dependendo da versão)
- Cadastro de produtos
- Carrinho de compras
- Finalização de pedido

## 📂 Estrutura
- `EcommerceNexo-Manual/` → versão com SQL na unha
- `EcommerceNexo-EF/` → versão com Entity Framework
