# 📚 Library Management API  

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/)  
[![Build](https://img.shields.io/badge/build-passing-brightgreen)]()  
[![Tests](https://img.shields.io/badge/tests-6%20passing-success)]()  
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)  

A **Library Management API** built with **ASP.NET Core 8.0**, designed to manage books, authors, and users in a library system.  
This project demonstrates **clean architecture, RESTful design, unit testing with xUnit, and integration testing using Postman**.  

---

## 🚀 Features  

- Manage **Books** (CRUD operations).  
- Manage **Authors**.  
- Manage **Library Members**.  
- RESTful endpoints following best practices.  
- Unit testing with **xUnit** & **EF Core In-Memory database**.  
- API testing with **Postman collection**.  

---

## 🛠️ Tech Stack  

- **.NET 8.0**  
- **ASP.NET Core Web API**  
- **Entity Framework Core** (InMemory provider for testing)  
- **xUnit** for unit testing  
- **Postman** for API testing  
- **Swagger/OpenAPI** for API documentation  

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
│   ├── Swagger.json           # Postman collection for API testing
│   └── UnitTests/             # Saved unit test results
│
├── README.md                  # Project documentation
└── Library_Management_API.sln # Solution file
```

---

## ⚙️ Prerequisites  

- Install [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- Install [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)  
- (Optional) Install [Postman](https://www.postman.com/downloads/) for API testing  

---

## 🏗️ Build & Run  

### 1️⃣ Clone the repository  
```bash
git clone https://github.com/sajidperwez2001/Library_Management_API.git
cd Library_Management_API
```

### 2️⃣ Restore dependencies  
```bash
dotnet restore
```

### 3️⃣ Build the solution  
```bash
dotnet build
```

### 4️⃣ Run the API  
```bash
cd src/Library.Api
dotnet run
```

API will start at:  
👉 `https://localhost:5001/swagger` (Swagger UI)  
👉 `http://localhost:5000/swagger`  

---

## 🧪 Testing  

### Run Unit Tests  
From the solution root:  
```bash
dotnet test
```

✔️ All tests should pass — results are stored under `/docs/UnitTests`.  

### Postman Tests  
1. Open Postman.  
2. Import the collection: `Docs/Swagger/Swagger.json`.  
3. Run the requests against the API.  

---

## 📊 Test Results  

- ✅ **6 unit tests** included with xUnit (passing).  
- 🧪 Postman collection demonstrates successful CRUD testing.  
- Detailed test results available in `Docs/UnitTests/`.  

---

## 📘 Documentation  

Swagger/OpenAPI available once the API is running:  
- `https://localhost:5001/swagger`  
- `http://localhost:5000/swagger`  

---

## 👨‍💻 Author  

**Mohammad Sajid Perwez**  
- 💼 Senior Full Stack .NET Developer | Fintech | AI-Assisted Development  
- 📧 sajid.perwez2001@gmail.com  
- 🌐 [LinkedIn Profile](https://www.linkedin.com/in/sajidperwez/)  

---

## 📝 License  

This project is open-source and available under the [MIT License](LICENSE).  
