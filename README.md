Library Management System Web API
===================================
Please refer src/Library.Api for the API and src/Library.Tests for tests.

High-level Architecture
========================================================================
API Layer — Controllers

Auth Layer — Authentication & Authorization (JWT token issuance, role claims)

Service Layer — Business logic

Data Layer — EF Core DbContext & Repositories

Domain — Entities & DTOs

Tech Stack
================================================
Language: C#

Framework: ASP.NET Core Web API (net8.0 recommended)

ORM: Entity Framework Core (Code-First recommended)

Database: Microsoft SQL Server

Auth: JWT (System.IdentityModel.Tokens.Jwt + Microsoft.AspNetCore.Authentication.JwtBearer)

Docs & Testing: Swagger (Swashbuckle), xUnit for unit tests

Notes:
============================================================
**Modify DefaultConnection as per your Microsoft SQL Server in appsettings.json
**Create database
dotnet ef migrations add InitialCreate
dotnet ef database update
**Unit Test
dotnet clean
dotnet restore
dotnet build
dotnet test
**Run LibraryAPI
dotnet run --project src\Library.Api

Endpoints:
============================================================
{{baseUrl}}/api/Auth/login
{{baseUrl}}/api/Auth/register
{{baseUrl}}/api/Books
{{baseUrl}}/api/Books/:id
{{baseUrl}}/api/Loans/borrow
{{baseUrl}}/api/Loans



