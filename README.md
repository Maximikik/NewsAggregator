# NewsAggregator

A news aggregation backend built with **ASP.NET Core (.NET 10)** and **PostgreSQL**. It collects articles from RSS sources on a schedule, organises them by category, and serves them through a REST API with JWT authentication, a personalised feed, and Redis caching.

The project is built around **Clean Architecture** and **CQRS**, and is the backend for a companion Angular client (in progress).

## Features

- **RSS import:** a recurring background job (Hangfire) fetches new articles from registered sources every 5 minutes
- **Sources, categories, articles:** create and query each resource through the API
- **Authentication:** registration and login with JWT access tokens and revocable refresh tokens; passwords hashed with BCrypt
- **Personalised feed:** an authenticated endpoint that serves articles for the current user
- **Likes:** authenticated users can like articles
- **Caching:** query results cached through a pipeline behaviour; Redis or in-memory cache selectable by configuration
- **Observability:** structured logging with Serilog (console and rolling file), request performance and error logging behaviours
- **API docs:** Swagger UI in development, plus a ready-made Postman collection in `postman/`
- **Docker:** Dockerfile for the Web API

## Tech stack

| Area | Technology |
|---|---|
| Runtime | .NET 10, ASP.NET Core Minimal APIs |
| Data | Entity Framework Core 10, PostgreSQL (Npgsql), migrations |
| CQRS | Mediator (source generator), FluentValidation, Result pattern |
| Background jobs | Hangfire (PostgreSQL storage) |
| Caching | Redis (StackExchange.Redis) or in-memory |
| Auth | JWT bearer, refresh tokens, BCrypt.Net |
| Mapping | Mapperly (compile-time) |
| Feeds | System.ServiceModel.Syndication (RSS) |
| Logging | Serilog |
| API docs | Swagger / OpenAPI |
| Containers | Docker |

## Architecture

The solution follows Clean Architecture; dependencies point inwards.

```
NewsAggregator.WebAPI            Minimal API endpoints, request contracts, auth/CORS/Swagger setup
NewsAggregator.Application       Use cases (commands/queries), validators, pipeline behaviours, interfaces
NewsAggregator.Domain            Entities, domain events, EF Core configurations
NewsAggregator.Infrastructure    EF Core DbContext, migrations, JWT, Redis/memory cache, RSS parser, Hangfire jobs
```

Key design decisions:

- **CQRS with pipeline behaviours:** every command and query passes through validation, caching, performance monitoring and error logging behaviours, so cross-cutting concerns live in one place.
- **Result pattern:** handlers return explicit success/error results that the API layer maps to HTTP responses, instead of throwing exceptions for expected failures.
- **Domain events:** entities raise events (for example, article imported or liked) that are handled through a domain event dispatcher.
- **Feature folders:** the Application layer is organised by feature (Articles, Categories, Sources, Users), each with its own commands, queries and events.

## API overview

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/users/register` | Register a user | no |
| POST | `/users/login` | Log in, receive access and refresh tokens | no |
| POST | `/users/refresh` | Exchange a refresh token for new tokens | no |
| POST | `/users/logout` | Revoke a refresh token | no |
| GET | `/users/feed` | Personalised article feed | yes |
| GET | `/articles` | List articles | no |
| GET | `/articles/{id}` | Get an article | no |
| POST | `/articles` | Create an article | no |
| POST | `/articles/import` | Import articles from a source | no |
| POST | `/articles/{articleId}/like` | Like an article | yes |
| GET | `/categories` | List categories | no |
| POST | `/categories` | Create a category | no |
| GET | `/sources` | List sources | no |
| GET | `/sources/{id}` | Get a source | no |
| POST | `/sources` | Create a source | no |

Swagger UI describes the exact request and response shapes when the app runs in Development.

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL
- Redis (optional; the app can use an in-memory cache instead)

### Configuration

Secrets are not stored in the repository. Set them with [user secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets) or environment variables:

```bash
cd NewsAggregator.WebAPI
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=newsaggregator;Username=postgres;Password=<password>"
dotnet user-secrets set "Jwt:Issuer" "newsaggregator"
dotnet user-secrets set "Jwt:Audience" "newsaggregator-clients"
dotnet user-secrets set "Jwt:SecretKey" "<a-long-random-secret-at-least-32-characters>"
dotnet user-secrets set "Jwt:ExpirationMinutes" "30"
dotnet user-secrets set "Cors:AllowedOrigins:0" "http://localhost:4200"
```

Optional switch:

| Setting | Effect |
|---|---|
| `UseRedisCache` = `true` | Use Redis (also set `ConnectionStrings:Redis`); otherwise in-memory cache is used |

### Run

```bash
dotnet run --project NewsAggregator.WebAPI
```

In Development the app applies database migrations on startup and exposes:

- Swagger UI at `/swagger`
- Hangfire dashboard at `/hangfire`

### Run with Docker

```bash
docker build -t newsaggregator -f NewsAggregator.WebAPI/Dockerfile .
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="<connection string>" \
  -e Jwt__Issuer="newsaggregator" \
  -e Jwt__Audience="newsaggregator-clients" \
  -e Jwt__SecretKey="<secret>" \
  -e Jwt__ExpirationMinutes=30 \
  newsaggregator
```

## Roadmap

- Angular client (in progress)
- Automated tests
- Docker Compose setup with PostgreSQL and Redis

## Author

**Maksim Rudalouski**, C#/.NET and Angular developer, Minsk, Belarus
[LinkedIn](https://www.linkedin.com/in/maksim-rudalouski-441a63221) · mrudalovskiy@gmail.com
