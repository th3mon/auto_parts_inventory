# AutoParts Inventory
## Weekendowy projekt przygotowawczy do pracy w Moto-Profil

**Cel:** mini onboarding do Moto-Profil, nie kolejny duży projekt do portfolio. Czas: 8-12 godzin. Projekt ma oswoić domenę motoryzacyjną i ekosystem .NET, wykorzystując znajomość Reacta, TypeScriptu i PostgreSQL.

## 1. Koncepcja

Prosty system zarządzania magazynem części samochodowych i zamówieniami. Pracownik hurtowni wyszukuje część, sprawdza dostępność, składa zamówienie i obserwuje zmianę stanu magazynowego. To odpowiednik wcześniejszego Inventory Management System, tym razem w C# zamiast Expressa i Prismy.

Przykładowe dane:

| Część | SKU | Cena | Stan |
|---|---|---:|---:|
| Filtr oleju | FO-001 | 35 zł | 42 |
| Klocki hamulcowe | KH-002 | 129 zł | 3 |
| Świeca zapłonowa | SZ-003 | 28 zł | 18 |

Nie potrzebujesz zdjęć produktów ani rozbudowanego dashboardu.

## 2. Stack technologiczny

| Technologia | Zastosowanie | Priorytet |
|---|---|---|
| C# | Logika biznesowa | Najwyższy |
| ASP.NET Core .NET 8 | REST API | Najwyższy |
| Entity Framework Core 8 | ORM, migracje | Wysoki |
| PostgreSQL | Baza danych | Wysoki |
| SQL | Zapytania, relacje, transakcje | Wysoki |
| React + TypeScript | Prosty frontend | Średni |
| xUnit | Testy backendu | Wysoki |
| Docker Compose | Infrastruktura | Średni |
| RabbitMQ | Komunikacja asynchroniczna | Opcjonalny |

.NET 8 jest wersją przyjętą w planie przygotowawczym. EF Core, xUnit i podział serwisów to propozycje edukacyjne, a nie potwierdzona architektura Moto-Profil. Treść oryginalnej oferty nie została ponownie zweryfikowana przy eksporcie.

## 3. Architektura

```text
              React + TypeScript
                 /          \
           Catalog API     Orders API
                |          /    |
                |   HTTP <-     |
                |               |
           catalog_db       orders_db
              PostgreSQL (jedna instancja)

       RabbitMQ: opcjonalna komunikacja zdarzeń
```

Dwa niezależne API i osobne bazy logiczne na jednej instancji PostgreSQL. Najpierw zbuduj Catalog API, następnie Orders API. RabbitMQ zostaw na koniec.

## 4. Zakres funkcjonalny MVP

### Catalog API

```csharp
public class Part
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = "";
    public string Name { get; set; } = "";
    public string Manufacturer { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
```

| Metoda | Endpoint | Funkcja |
|---|---|---|
| GET | /api/parts | Lista, wyszukiwanie |
| GET | /api/parts/{id} | Szczegóły |
| POST | /api/parts | Dodanie |
| PUT | /api/parts/{id} | Aktualizacja |
| DELETE | /api/parts/{id} | Usunięcie |
| POST | /api/parts/{id}/reserve | Rezerwacja towaru |

Przećwicz DTO, walidację, Dependency Injection, LINQ, async/await, obsługę błędów HTTP i EF Core.

### Orders API

```csharp
public class Order
{
    public Guid Id { get; set; }
    public Guid PartId { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Pending,
    Confirmed,
    Rejected
}
```

Endpointy: `POST /api/orders` oraz `GET /api/orders`.

### Przepływ zamówienia

1. Pracownik zamawia 5 sztuk filtrów oleju.
2. Orders API wywołuje Catalog API przez HTTP, używając HttpClient.
3. Catalog API sprawdza dostępność i rezerwuje 5 sztuk w bazie.
4. Zamówienie otrzymuje status Confirmed lub Rejected, jeśli brakuje towaru.
5. Frontend odświeża zamówienia i stan magazynowy.

**Wyzwanie:** jak zagwarantować, że dwóch pracowników nie zamówi jednocześnie ostatniej sztuki? Przećwicz transakcje, współbieżność i atomowe operacje SQL. W pełnym systemie trzeba również obsłużyć awarię między rezerwacją a zapisem zamówienia (np. idempotencja i kompensacja); na weekend wystarczy opisać to ograniczenie.

## 5. Harmonogram

### Piątek (1-2 godziny)

- [ ] Utworzenie solution i ASP.NET Core Web API
- [ ] PostgreSQL w Dockerze
- [ ] Encja Part, DbContext i pierwsza migracja
- [ ] Działający GET /api/parts

### Sobota (4-5 godzin)

- [ ] CRUD części
- [ ] DTO, walidacja i obsługa błędów
- [ ] Wyszukiwanie, filtrowanie i paginacja z LINQ
- [ ] Rezerwacja stanu magazynowego
- [ ] Testy jednostkowe xUnit
- [ ] Prosty frontend React: lista i formularz

### Niedziela (3-5 godzin)

- [ ] Orders API
- [ ] Komunikacja przez HttpClient
- [ ] Test integracyjny zamówienia
- [ ] Lista zamówień na frontendzie
- [ ] Docker Compose i README
- [ ] Opcjonalnie: RabbitMQ

## 6. Struktura repozytorium

```text
autoparts-inventory/
├── AutoParts.sln
├── src/
│   ├── Catalog.Api/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Data/
│   │   └── Program.cs
│   ├── Orders.Api/
│   │   ├── Controllers/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── Data/
│   │   └── Program.cs
│   └── frontend/
│       ├── src/
│       └── package.json
├── tests/
│   ├── Catalog.Tests/
│   └── Orders.Tests/
├── docker-compose.yml
└── README.md
```

Nie komplikuj projektu Clean Architecture, CQRS ani MediatR.

## 7. Priorytety nauki

| To już znasz | Tego warto się nauczyć |
|---|---|
| Express | ASP.NET Core Controllers |
| Prisma | EF Core, DbContext, LINQ |
| Zod | DTO i Data Annotations |
| Middleware Express | Middleware ASP.NET Core |
| Promise, async/await | Task, async/await, CancellationToken |
| Vitest + Supertest | xUnit + WebApplicationFactory |
| Fetch / Axios | HttpClient, IHttpClientFactory |
| TypeScript interfaces | C# interfaces i records |

**Podział czasu:** 75% backend, 15% frontend, 10% infrastruktura.

Najważniejszy rezultat: działający CRUD, PostgreSQL, kilka testów i prosty frontend. Drugi serwis jest kolejnym etapem, RabbitMQ bonusem. Jeśli zabraknie czasu, ogranicz zakres do Catalog API i rezerwacji magazynowej.

Buduj projekt etapami, samodzielnie pisząc kod C# i porównując mechanizmy z Node.js i TypeScriptem. Celem jest przygotowanie do pracy, nie samo ukończenie repozytorium.
