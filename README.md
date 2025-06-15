# 🚀 NLW Unite – C# Backend

API desenvolvida durante a **Next Level Week Unite** da Rocketseat, utilizando **C#** e **.NET Core**, para gerenciar eventos, participantes e realizar check‑ins.

## 📚 Descrição

Esse backend permite:

- ✅ Criar eventos (com capacidade máxima);
- 📋 Registrar participantes por evento;
- 🎫 Realizar check‑in dos participantes;
- 🔍 Listar eventos, participantes e status de check‑in.

## 🛠️ Tecnologias utilizadas

- Linguagem: **C#**
- Framework: **.NET Core / ASP.NET Core**
- ORM: **Entity Framework Core**
- Banco de dados: **SQLite**
- Documentação da API: **Swashbuckle / Swagger**
- Arquitetura: organização em camadas (API, Application, Infrastructure, etc.)

## 🧩 Estrutura do projeto

Pasta principal: `PassIn`

- `PassIn.Api/` – camada que expõe os endpoints REST.
- `PassIn.Application/` – lógica de aplicação, serviços e casos de uso.
- `PassIn.Infrastructure/` – configurações de banco e repositórios.
- `PassIn.Communication/` – DTOs e objetos de transferência.
- `PassIn.Exceptions/` – tratamento de erros personalizados.

