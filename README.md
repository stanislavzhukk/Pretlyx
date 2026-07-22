# Pretlyx
 .NET 10 | C# 14 | Aspire 13 | EF Core 10 | xUnit v3

A dog-walking platform — owners find and book walkers, walkers manage their schedule and get discovered by nearby owners with dogs.

Built on top of a Clean Architecture starter template. Zero commercial dependencies, everything set up to ship features from day one.

## Status

Early stage — architecture and auth are in place, domain features are being built stage by stage.

| Stage | Feature | Status |
|-------|---------|--------|
| 1 | Project foundation (Clean Architecture, CQRS, EF Core, Identity/JWT, PostgreSQL, Aspire) | ✅ Done |
| 2 | User profiles (Owner / Walker) | 🟡 In progress — create endpoints for `OwnerProfile` and `WalkerProfile` exist; update, avatar upload, and mode switching not yet built |
| 3 | Dogs | ⬜ Not started |
| 4 | Walker availability / schedule | ⬜ Not started |
| 5 | Slot generation | ⬜ Not started |
| 6 | Walker search | ⬜ Not started |
| 7 | Booking | ⬜ Not started |
| 8 | Booking history | ⬜ Not started |
| 9 | Reviews | ⬜ Not started |
| 10 | Favorites | ⬜ Not started |
| 11 | Notifications | ⬜ Not started |
| 12 | Chat | ⬜ Not started |
| 13 | Admin | ⬜ Not started (Identity role + seeded admin already scaffolded by the template) |

Payments, promo codes, push notifications, live walker location tracking, walk photo reports, subscriptions, and calendar sync are intentionally out of scope for now.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| **Architecture** | Clean Architecture (Domain, Application, Infrastructure, Api) |
| **Runtime** | .NET 10 / C# 14 |
| **API** | Minimal APIs with TypedResults |
| **CQRS** | Manual handlers — zero dependencies, zero licensing risk |
| **Validation** | FluentValidation 12 + Result pattern |
| **Errors** | ProblemDetails (RFC 9457) + global exception handler |
| **Database** | EF Core 10 + PostgreSQL |
| **Caching** | Microsoft HybridCache (L1 in-memory + L2 Redis) |
| **Auth** | ASP.NET Identity + JWT with refresh tokens |
| **API Docs** | Scalar (modern OpenAPI UI) |
| **Logging** | Serilog 10 structured logging |
| **Observability** | .NET Aspire 13 + OpenTelemetry (traces, metrics, logs) |
| **Testing** | xUnit v3 + FluentAssertions + NSubstitute + NetArchTest |
| **Solution** | `.slnx` format + Central Package Management |

## Architecture

```
┌──────────────────────────────────────────────────┐
│                    Api Layer                      │
│         Endpoints, Program.cs, Scalar            │
└──────────────────┬───────────────────────────────┘
                   │ depends on
┌──────────────────▼───────────────────────────────┐
│              Infrastructure Layer                 │
│     EF Core, Identity, JWT, HybridCache          │
└──────────────────┬───────────────────────────────┘
                   │ depends on
┌──────────────────▼───────────────────────────────┐
│              Application Layer                    │
│      CQRS Handlers, Validators, DTOs             │
└──────────────────┬───────────────────────────────┘
                   │ depends on
┌──────────────────▼───────────────────────────────┐
│                Domain Layer                       │
│      Entities, Result, Repositories (interfaces) │
└──────────────────────────────────────────────────┘
```

**Dependency rule:** Each layer only depends on the layer below it. Domain has zero external dependencies. Architecture tests enforce this at build time.

## Domain Model (current + planned)

- `OwnerProfile` / `WalkerProfile` — one-to-one with `User` (Identity), 🟡 in progress
- `Dog` — belongs to an `OwnerProfile`, planned
- `AvailabilityRule` / `AvailabilityException` — a walker's recurring schedule + one-off exceptions (vacation, extra day), planned
- `Booking` — the core aggregate; `Pending → Accepted/Rejected → Completed`, with `Cancelled*` branches, planned
- `Review`, `FavoriteWalker`, `Notification`, `ChatMessage`, `Complaint` — later stages

Slots are **not** a persisted entity — they're computed on the fly from `AvailabilityRule`/`AvailabilityException` minus overlapping active `Booking`s, to avoid pre-materializing a slot table for every walker for months ahead.

## Project Structure

```
├── src/
│   ├── Domain/           # Entities, value objects, repository interfaces
│   ├── Application/       # CQRS commands/queries, handlers, validators
│   ├── Infrastructure/    # EF Core, Identity, JWT, caching, repositories
│   ├── Api/               # Minimal API endpoints, Scalar, middleware
│   ├── AppHost/           # Aspire orchestration (PostgreSQL + Redis)
│   └── ServiceDefaults/   # OpenTelemetry, health checks, resilience
├── tests/
│   ├── Architecture.Tests/     # Dependency rule enforcement
│   └── Application.UnitTests/  # Handler unit tests
├── Directory.Build.props                         # .NET 10, C# latest, nullable enabled
├── Directory.Packages.props                      # Central Package Management
├── .editorconfig                                 # Code style rules
├── docker-compose.yml                            # PostgreSQL + Redis (standalone, no Aspire)
└── README.md
```

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for Aspire containers)

### Run with Aspire (recommended)

```bash
cd src/AppHost
dotnet run
```

This starts everything:
- **PostgreSQL** database with pgAdmin
- **Redis** cache with RedisInsight
- **API** with auto-migration and seed data
- **Aspire Dashboard** for OpenTelemetry (traces, metrics, logs)

Open the Aspire Dashboard URL from the console output to see your telemetry.

### Run without Aspire

```bash
# Start dependencies
docker compose up -d

# Run the API
cd src/Api
dotnet run
```

### Explore the API

Navigate to `https://localhost:7200/scalar/v1` for the interactive Scalar API docs.

**Default admin credentials** (seeded automatically):
- Email: `admin@example.com`
- Password: `Admin123!`

### Run Tests

```bash
cd src
dotnet test Petlyx.slnx
```

### Adding a New Feature

Follow the Todos / Profiles pattern:

1. **Domain** — Add your entity in `Domain/Entities/`
2. **Application** — Create a feature folder in `Application/Features/YourFeature/` with command/query records, handlers, and validators
3. **Infrastructure** — Add EF Core configuration in `Infrastructure/Persistence/Configurations/` and repository in `Infrastructure/Persistence/Repositories/`
4. **Api** — Add endpoints in `Api/Endpoints/` and register in `Program.cs`

## Key Design Decisions

| Decision | Why |
|----------|-----|
| **Manual CQRS** over MediatR | Zero licensing risk. MediatR is commercial since v13. You learn the pattern, not a library. |
| **Scalar** over Swagger UI | Modern, faster, better UX. Swagger UI is legacy. |
| **HybridCache** over IMemoryCache | Built-in stampede protection, L1+L2 cache, automatic serialization. |
| **Result pattern** over exceptions | Explicit error handling, no hidden control flow, better API contracts. |
| **Manual handler registration** over Scrutor | Zero dependencies for DI scanning. Assembly reflection is 40 lines of code. |
| **.slnx** over .sln | XML-based, merge-friendly, smaller, future of .NET solutions. |
| **Slots computed, not stored** | Avoids materializing and invalidating a slot table for every walker on every schedule change; recomputed on read, cached with a short TTL. |
| **Price frozen on booking (`PriceAtBooking`)** | A walker changing their rate later must not retroactively change the price of an existing booking. |
