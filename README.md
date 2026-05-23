# LS Salto — Sistema de Gestão de Designações Congregacionais

Sistema full-stack para gerenciar designações de reunião de uma congregação. Permite que publicadores visualizem suas designações e que anciãos gerenciem tudo em lote.

---

## Funcionalidades

- **Login com perfis distintos:** Ancião (administrador) e Publicador (visualização)
- **Gestão de Publicadores:** cadastro, edição, exclusão, cargo e privilégios
- **Grupos:** visualização dos membros por grupo (Salto e Itu) com filtros
- **Designações:** módulos em desenvolvimento para Reunião MWB, Reunião Domingo, Limpeza, Mecânicas e Pregação

---

## Stack

| Camada | Tecnologia |
|---|---|
| Backend | .NET 10, Clean Architecture, MediatR, FluentValidation |
| Banco de dados | SQL Server Express 2025 |
| ORM | Entity Framework Core (Code-First) |
| Frontend | Angular 19 (Standalone Components, Signals, Angular Material) |
| Autenticação | JWT com Roles (`Anciao`, `Publicador`) |

---

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Angular CLI 19](https://angular.io/cli): `npm install -g @angular/cli`
- SQL Server Express com instância `LSalto`

---

## Configuração do banco de dados

O projeto usa autenticação Windows (Trusted Connection). Certifique-se de que a instância `LSalto` está rodando e aplique as migrations:

```bash
cd backend
dotnet ef database update --project src/LSalto.Infrastructure --startup-project src/LSalto.API
```

A connection string padrão está em `backend/src/LSalto.API/appsettings.json`:

```
Server=localhost\LSalto;Database=LSaltoDB;Trusted_Connection=True;TrustServerCertificate=True;
```

---

## Como rodar

### Backend (API)

```bash
cd backend
dotnet run --project src/LSalto.API
```

A API sobe em `https://localhost:7xxx` (a porta exata aparece no terminal).

### Frontend

```bash
cd frontend/lsalto-web
npm install
ng serve
```

O frontend fica disponível em `http://localhost:4200`.

---

## Estrutura de pastas

```
ls-salto-site/
├── backend/
│   └── src/
│       ├── LSalto.Domain/          ← Entidades e enums
│       ├── LSalto.Application/     ← Use Cases, DTOs, validações
│       ├── LSalto.Infrastructure/  ← EF Core, migrations, banco
│       └── LSalto.API/             ← Controllers, JWT, Program.cs
│   └── tests/
│       └── LSalto.Tests/           ← Testes unitários (xUnit)
└── frontend/
    └── lsalto-web/
        └── src/app/
            ├── core/               ← Serviços, guards, interceptors
            ├── shared/             ← Componentes reutilizáveis
            └── features/           ← Módulos da aplicação
```

---

## Testes

```bash
cd backend
dotnet test
```

26 testes unitários cobrindo validators e handlers de Publicadores e Auth.

---

## Licença

Projeto pessoal de estudo. Todos os direitos reservados.
