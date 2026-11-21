# AutoParts Inventory

A small learning project for managing automotive parts inventory and orders, focused on C#, ASP.NET Core, and PostgreSQL.

The goal is to practice the .NET ecosystem by building a practical backend with a minimal React interface—not a production-ready warehouse management system.

> **Status:** Planning stage. This repository currently contains the project plan; application code, tests, and infrastructure have not been implemented yet.

## Planned Features

- Create, browse, update, and delete automotive parts.
- Search, filter, and paginate the parts catalog.
- Track stock quantities and reserve available inventory.
- Place orders and view their status.
- Handle concurrent stock reservations safely.

## Planned Technology Stack

| Area | Technology |
| --- | --- |
| Backend | C#, ASP.NET Core (.NET 8) |
| Data access | Entity Framework Core 8 |
| Database | PostgreSQL |
| Frontend | React, TypeScript |
| Testing | xUnit, ASP.NET Core integration tests |
| Infrastructure | Docker Compose |
| Optional messaging | RabbitMQ |

## Architecture

The initial milestone is a standalone **Catalog API** with CRUD operations and stock reservations. An **Orders API** is planned as the next step.

```text
React + TypeScript
    |          |
Catalog API <-- Orders API
    |    HTTP      |
catalog_db     orders_db
    \             /
     PostgreSQL
```

The APIs will use separate logical databases within one PostgreSQL instance. RabbitMQ is an optional extension, not a requirement for the initial implementation.

The order flow will reserve stock through the Catalog API before confirming an order. Handling failures between stock reservation and order persistence is a known distributed-systems challenge to address or document explicitly.

## Roadmap

- [ ] Set up the .NET solution and PostgreSQL.
- [ ] Implement the parts catalog, validation, and persistence.
- [ ] Add search, filtering, pagination, and stock reservations.
- [ ] Cover core behavior with automated tests.
- [ ] Build a minimal React interface.
- [ ] Add the Orders API and HTTP-based service communication.
- [ ] Add Docker Compose and local setup instructions.
- [ ] Explore asynchronous messaging with RabbitMQ (optional).

## Documentation

The detailed project plan is available in Polish:

- [Project plan (Markdown)](docs/AutoParts-Inventory.md)
- [Project plan (PDF)](docs/AutoParts-Inventory.pdf)

Local development commands will be added once the application scaffold is in place.

## License

This project is licensed under the [GNU General Public License v3.0](LICENSE).
