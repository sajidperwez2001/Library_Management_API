# 📚 Library Management API

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![Entity Framework Core](https://img.shields.io/badge/EF%20Core-8.0-512BD4?logo=nuget)](https://learn.microsoft.com/ef/core/)
[![Build](https://img.shields.io/badge/build-passing-brightgreen)]() 
[![xUnit](https://img.shields.io/badge/Testing-xUnit-orange?logo=xunit)](https://xunit.net/)
[![Tests](https://img.shields.io/badge/tests-100%25-success)]() 
[![JWT](https://img.shields.io/badge/Auth-JWT-green?logo=jsonwebtokens)](https://jwt.io/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

---

## 📖 Overview

The **Library Management API** is a backend system built with **ASP.NET Core 8.0**.  
It allows managing library operations such as:

- Managing **Books** (add, update, delete, search)  
- Handling **Loans** (borrow/return books)  
- Managing **Users** (with secure authentication using JWT)  
- Role-based access to protect resources  

It is designed to be clean, extensible, and production-ready with unit tests and API documentation.

---

## ✨ Features
- Manage **Books**, **Loans**, and **Users**
- **JWT-based Authentication & Authorization**
- RESTful endpoints built using **ASP.NET Core Web API**
- **Entity Framework Core** with InMemory and SQL Server support
- **xUnit** unit tests with **Moq**
- **Postman collection** for API testing
- **Swagger UI** for easy API documentation & exploration

---

## 🚀 Tech Stack

- **.NET 8.0** – Backend framework  
- **ASP.NET Core Web API** – REST API development  
- **Entity Framework Core 8.0** – ORM for database access  
- **SQL Server** – Database (can be swapped with others)  
- **JWT (JSON Web Tokens)** – Authentication and authorization  
- **xUnit** – Unit testing framework  
- **Moq** – Mocking dependencies in tests  
- **Coverlet** – Code coverage collection  
- **Swagger / OpenAPI** – API documentation and testing  
- **Postman** – API testing tool  

---

## 🛠️ Prerequisites

Before you start, make sure you have installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [VS Code](https://code.visualstudio.com/)  
- [Postman](https://www.postman.com/downloads/) for API testing  
- [Git](https://git-scm.com/)  

---

## ⚙️ Setup & Configuration Guide

Follow these steps to clone, configure, and run the project.

### 1. Clone the Repository
git clone https://github.com/sajidperwez2001/Library_Management_API.git
cd Library_Management_API

### 2. Restore Dependencies
dotnet restore

### 3. Configure Database Connection

Open src/Library.Api/appsettings.json

Update the connection string for your SQL Server:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

### 4. Apply EF Core Migrations
dotnet ef migrations add InitialCreate -p src/Library.Api -s src/Library.Api
dotnet ef database update -p src/Library.Api -s src/Library.Api

### 5. Build the Solution
dotnet build

### 6.Run Unit Tests
dotnet test

### 7. Run the API
dotnet run --project src/Library.Api

The API will start at: **https://localhost:5001** (or http://localhost:5000)

### 8. API Documentation (Swagger)
Once the API is running, open:
```
https://localhost:5001/swagger/index.html
```
to explore and test endpoints directly.

---

## 🔑 Authentication (JWT)
- API endpoints are **secured with JWT tokens**.  
- To access protected endpoints:
  1. Use the `/auth/login` endpoint to generate a token (valid credentials required).
  2. Copy the JWT token.
  3. Add it to the **Authorization header** in Postman or Swagger:
     ```
     Authorization: Bearer <your-token>
     ```

---

## 📬 API Testing (Postman)

1. Start the API (dotnet run).

2. Import the Postman collection from:
   ```
   Docs/Swagger/Swagger.json
   ```
3. Set the base URL:
   ```
   https://localhost:5001
   ```
4. Use the **Login request** to fetch a JWT token.
   ```
   Login → /api/auth/login (get JWT token)
   ```
5. Apply the token to subsequent API requests.

6. Run the requests:
  ```
  Register a User → /api/auth/register
  ```
  ```  
  Books API → /api/books (CRUD operations)
  ```
  ```  
  Loans API → /api/loans (borrow/return books)
  ```
---

## 🧪 Unit Testing

All tests are located in src/Library.Tests

Tests include AuthService, BookService, and LoanService

Run tests:

dotnet test


Output Example:

Test summary: total: 6, failed: 0, succeeded: 6, skipped: 0

---

## 📊 Code Coverage

Run tests with coverage:

dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov


You can visualize results with ReportGenerator.

---

## 📂 Project Structure
```
Library_Management_API/
│
├── src/
│   ├── Library.Api/           # Main API project
│   └── Library.Tests/         # Unit tests
│
├── docs/
│   ├── Swagger/               # Postman collection for API testing
│   └── UnitTests/             # Saved unit test results
│
├── README.md                  # Project documentation
└── Library_Management_API.sln # Solution file
```
---

## 📌 Future Improvements

Dockerize the API & DB

Add CI/CD pipeline (GitHub Actions)

Enhance role-based policies (Admin/User/Librarian)

Add caching & performance optimizations

---

## 📜 License

This project is licensed under the MIT License.
Feel free to use, modify, and distribute as needed.

