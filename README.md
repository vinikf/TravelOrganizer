# TravelOrganizer

Organize suas viagens de forma inteligente! O **TravelOrganizer** é uma aplicação web desenvolvida em .NET 8 que permite criar roteiros personalizados, integrar com Google Agenda, utilizar recursos de IA para sugestões de passeios e muito mais.

## ✈️ Funcionalidades

- Cadastro e autenticação de usuários (com confirmação por e-mail)
- Criação e gerenciamento de viagens e roteiros personalizados
- Adição de viajantes e compartilhamento de viagens
- Integração planejada com Google Agenda
- Geração de roteiros inteligentes usando IA (em desenvolvimento)
- API RESTful pronta para integração com front-end (incluindo mapas)

## 🏗️ Arquitetura

O projeto segue uma arquitetura limpa, separando responsabilidades em camadas:

- **Domain**: Entidades, DTOs e regras de negócio
- **Application**: Serviços de aplicação e lógica de uso
- **Infrastructure**: Persistência de dados (Entity Framework Core)
- **Api**: Controllers e endpoints REST

## 🗂️ Principais Entidades

- **Usuario**: Usuário autenticado do sistema
- **Viagem**: Representa uma viagem criada pelo usuário
- **Roteiro**: Detalha o roteiro de uma viagem (dias, etapas, atividades)
- **Viajante**: Participantes de uma viagem (usuário ou convidados)

## 🚀 Como rodar o projeto

1. **Pré-requisitos**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - SQL Server LocalDB (ou ajuste a connection string em `appsettings.json`)

2. **Clone o repositório**
git clone https://github.com/vinikf/TravelOrganizer.git cd TravelOrganizer

3. **Restaure os pacotes**
dotnet restore

4. **Atualize o banco de dados**
dotnet ef database update

5. **Rode a aplicação**
dotnet run --project TravelOrganizer.Api

6. **Acesse a API**
- Por padrão, estará disponível em `https://localhost:5001` ou `http://localhost:5000`
- Utilize ferramentas como [Postman](https://www.postman.com/) para testar os endpoints

## 🔑 Autenticação

- Utiliza ASP.NET Identity com Bearer Token (JWT)
- Endpoints principais:
- `POST /api/Auth/Register` — Cadastro de usuário
- `POST /api/Auth/Login` — Login e obtenção de token
- `POST /api/Auth/Refresh` — Refresh do token
- `POST /api/Auth/Logout` — Logout

### Exemplo de payload para registro
POST /api/Auth/Register { "nome": "João", "sobrenome": "Silva", "email": "joao@email.com", "password": "SenhaForte123!", "dataNascimento": "1990-01-01" }

### Exemplo de payload para login
POST /api/Auth/Login { "email": "joao@email.com", "password": "SenhaForte123!" }

## 📅 Integrações

- **Google Agenda**: Integração planejada para exportar roteiros (em desenvolvimento)
- **IA**: Geração de roteiros inteligentes (em desenvolvimento)
- **Mapas**: Endpoints preparados para integração com mapas no front-end (em desenvolvimento)

## 🛡️ Segurança

- Tokens JWT com refresh token rotativo
- Confirmação de e-mail no cadastro
- Proteção de endpoints sensíveis com `[Authorize]`

## 📝 Contribuindo

Contribuições são bem-vindas!  
Abra uma issue ou envie um pull request.

## 📄 Licença

Este projeto está sob a licença MIT.

---

Desenvolvido em .NET 8.