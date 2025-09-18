# GIS API (APPGiaoVat-main_API)

This repository contains a backend Web API for a GIS (Geographic Information System) / land records management application built with ASP.NET Core 5.0.

It provides REST endpoints for managing land parcels, documents, processes, user accounts, notifications and citizen feedback (phan anh), and includes features commonly needed for municipal GIS/record workflows.

## Key features

- ASP.NET Core 5 Web API project (`Gis.API`).
- JWT authentication support and optional Google/Facebook social login wiring.
- PostgreSQL (Npgsql) and Oracle EF Core providers referenced; the project uses a configurable connection string (via `AppSettings`).
- Swagger (Swashbuckle) for interactive API documentation at `/swagger`.
- SignalR hub for notifications (`/hubs/notification`).
- Background hosted service (`TimedHostedService`) for scheduled tasks.
- Health checks and HealthChecks UI (optional, commented in Startup).
- Static file server for uploaded files under `StaticFiles` served at `/StaticFiles`.
- Serilog logging and configurable Kestrel HTTPS settings.

## Project structure

- `Gis.API/` - Main Web API project (controllers, startup, middleware).
- `Gis.Core/` - Core domain models, interfaces and helpers.
- `Gis.Infrastructure/` - Infrastructure and data access implementations.
- `Gis.UnitTest/` - Unit tests (xUnit or MSTest project present).

Notable controllers: `Por_PhanAnhController`, `Por_ThuaDatController`, `Por_HoSoNguoiNopController`, `Sys_AccountController`, `Por_TemplateEmailController`, `Sys_FileController`, etc. Controllers follow a generic `ApiControllerBase<TEntity>` pattern for many domain entities.

## Tech stack

- .NET 5 (net5.0)
- ASP.NET Core 5 Web API
- Entity Framework Core (PostgreSQL via Npgsql, Oracle provider available)
- JWT authentication (Microsoft.AspNetCore.Authentication.JwtBearer)
- Swagger (Swashbuckle)
- SignalR
- Serilog
- HealthChecks

## Configuration

The application reads settings from `appsettings.json` / `appsettings.Development.json`. Important sections:

- `ConnectionString` / `AppSettings.ConnectionString` - database connection string used by EF Core.
- `IdentityServerAuthentication` - JWT/identity settings (Issuer, Audience, Secret, RequireHttpsMetadata).
- `Kestrel` - optional kestrel server configuration (HTTPS client cert mode, etc.).

Before running, update `Gis.API/appsettings.json` with a valid database connection and JWT secret.

## Running locally

Prerequisites:

- .NET 5 SDK (required to build and run the project)
- PostgreSQL or Oracle database (depending on which provider you configure)

Basic steps:

1. Open the solution in Visual Studio or build from the command line:

   dotnet build Gis.sln

2. From the `Gis.API` folder, run the API:

   dotnet run --project Gis.API/Gis.API.csproj

3. The API will expose Swagger at `http://localhost:5000/swagger` (or the configured Kestrel ports). SignalR notifications are available at `/hubs/notification`.

Notes:

- The project configures CORS with an `AllowAnyOrigin` policy that permits credentials. Adjust this for production.
- Health checks and HealthChecksUI are present but partially commented out in `Startup.cs`; enable them if you want an operational endpoint.

## Development tips

- Many controllers inherit from a generic `ApiControllerBase<TEntity>`, so adding new entities typically requires: model, DbSet in `DomainDbContext`, and a controller that extends `ApiControllerBase<YourEntity>`.
- EF Core migrations can be added/managed from the `Gis.API` project using the `dotnet ef` tooling.


