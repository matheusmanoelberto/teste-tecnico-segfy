# Segfy Insurance

Projeto para cadastro e gerenciamento de apolices de seguro automovel.

## Tecnologias

- Backend: .NET 8, ASP.NET Core, SQLite, EF Core para migrations e SQL puro no repository
- Frontend: React, TypeScript e Vite
- Testes: xUnit

## Arquitetura

```txt
backend
├── src
│   ├── SegfyInsurance.Api              # Controllers, Swagger, CORS e configuracao da API
│   ├── SegfyInsurance.Application      # Use cases, requisicoes e responses
│   ├── SegfyInsurance.Domain           # Entidades, enums e value objects
│   └── SegfyInsurance.Infrastructure   # SQLite, migrations e repositories
└── tests
    └── SegfyInsurance.Tests

frontend
└── segfy-insurance-web
    └── src
        ├── components
        ├── hooks
        ├── pages
        ├── routes
        ├── services
        ├── types
        ├── utils
        └── validations
```

## Como rodar

### Backend

```bash
cd backend
dotnet restore
dotnet run --project src/SegfyInsurance.Api
```

A API sobe em:

```txt
http://localhost:5062
```

Swagger:

```txt
http://localhost:5062/swagger
```

Ao iniciar, a API aplica as migrations e cria o banco SQLite em:

```txt
backend/src/SegfyInsurance.Api/segfy-insurance.db
```

### Frontend

```bash
cd frontend/segfy-insurance-web
npm install
npm run dev
```

O front abre em:

```txt
http://localhost:5173
```

Por padrao, o front chama a API em `http://localhost:5062`.

Para alterar a URL da API, crie um `.env` em `frontend/segfy-insurance-web`:

```txt
VITE_API_URL=http://localhost:5062
```

## Testes

```bash
cd backend
dotnet test
```

## Endpoints principais

```txt
POST   /api/apolices-seguro
GET    /api/apolices-seguro
GET    /api/apolices-seguro/{id}
PUT    /api/apolices-seguro/{id}
PATCH  /api/apolices-seguro/{id}/cancelar
DELETE /api/apolices-seguro/{id}
GET    /api/apolices-seguro/vencendo-proximos-30-dias
```

## Funcionalidades do front

- Cadastrar apolice
- Listar apolices
- Editar apolice
- Cancelar apolice
- Remover apolice
- Ver apolices vencendo nos proximos 30 dias
