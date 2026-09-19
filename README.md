# .NET Architecture Patterns Lab 🚀

A day-by-day, hands-on journey through the C# and .NET ecosystem — from language fundamentals through backend architecture patterns. This repository is my personal learning lab: every folder is a milestone, every commit is a day's progress, and every project builds on the concepts learned in the last one, deliberately progressing **controller-based CRUD → service layer → repository pattern → generic repository → (next) Unit of Work.**

> **Philosophy:** Learn by building. Instead of tutorials alone, each topic is reinforced with a small console exercise or a real feature shipped inside a Web API project.

---

## 🗺️ Learning Path

The projects are ordered the way they were actually learned — each one raises the difficulty and introduces new concepts on top of the previous stage.

| Stage | Project | Focus |
|---|---|---|
| 1 | [`FirstConsoleApp`](#1-firstconsoleapp) | C# fundamentals: variables, types, loops, arrays, strings, methods, exception handling |
| 2 | [`MasteringOOP`](#2-masteringoop) | The 4 pillars of OOP, LINQ, generics, and a full OOP mini-project |
| 3 | [`EcommerceWebApi`](#3-ecommercewebapi) | First ASP.NET Core Web API — controllers, DTOs, services, EF Core basics |
| 4 | [`EfCorePractice`](#4-efcorepractice) | Deep dive into Entity Framework Core — migrations, relationships, AutoMapper |
| 5 | [`TEcommerceWebApi`](#5-tecommercewebapi) | Production-style Web API — multi-table relations, analytics, pagination/filtering/sorting, global exception & performance middleware, first-pass generic repository |
| 6 | [`OrganizationManagement`](#6-organizationmanagement-current-focus) | Repository pattern refined — per-entity repositories (`IDepartmentRepository`, `IDesignationRepository`) built on a `GenericRepository<TEntity>` |
| Next | Unit of Work & Advanced Topics | Unit of Work, authentication/authorization, caching, clean architecture, testing, deployment |

---

## 📦 Projects

### 1. `FirstConsoleApp`
The starting point — pure C# fundamentals practiced in a console environment.

- Variables, data types, and type conversion (`TryParse`, casting)
- Control flow: loops, switch expressions & pattern matching
- Arrays: multi-dimensional and jagged arrays
- String manipulation
- Custom methods and input validation
- Exception handling: `try/catch/finally`, custom exceptions
- **Mini project:** a console-based Student Management system

### 2. `MasteringOOP`
A focused study of Object-Oriented Programming in C#.

- **Encapsulation** — private state, controlled access via properties
- **Inheritance** — `AuditableEntity`, `CustomerEntity`, `OrderEntity`
- **Polymorphism** — payment processors (`CreditCardProcessor`, `PayPalProcessor`, `CryptoProcessor`)
- **Abstraction & Interfaces** — notification services, discount services
- **Mini project:** an E-Commerce Shopping Cart engine combining all 4 pillars, dependency injection via constructors, custom exceptions, and LINQ aggregation for cart totals & tax calculation

### 3. `EcommerceWebApi`
First step into ASP.NET Core Web API development.

- REST API project structure: Controllers, Services, Interfaces, DTOs
- Entity Framework Core with a Postgres-backed `AppDbContext`
- AutoMapper for entity ↔ DTO mapping
- CRUD operations on a `Category` resource

### 4. `EfCorePractice`
A dedicated sandbox for mastering Entity Framework Core.

- Multiple entities (`Book`, `User`, `Order`, `Category`) and their relationships
- Iterative migrations (initial schema → column changes → type refinements)
- Query parameters and pagination helpers
- Service-layer patterns with interfaces

### 5. `TEcommerceWebApi`
The most feature-complete project in the lab — a production-style E-Commerce Web API built with **.NET 10**, applying everything learned so far.

**Core stack**
- ASP.NET Core Web API + Swashbuckle (OpenAPI/Swagger)
- Entity Framework Core with `Npgsql` (PostgreSQL)
- AutoMapper for DTO mapping
- Fluent API model configuration

**What's implemented**
- Multi-table relational schema: `User` → `Order` → `OrderItem` ← `Product` ← `Category`, with soft deletes
- Full CRUD for `Category`, `Product`, and `User`; order creation via checkout + order lookup by id / by user
- Query features: **pagination, searching, and sorting** via a centralized `QueryParameters` helper and enums (`SortOrder`)
- Analytics endpoints (`/api/v2/analytics`): category sales summary, top-selling products, customer spending, sales overview
- Global exception-handling middleware and a request performance-logging middleware
- Consistent `ApiResponse<T>` response envelope across all endpoints
- Layered architecture: `Controllers → Interfaces → Services → DbContext`, plus a first pass at a `GenericRepository<TEntity>` / `IGenericRepository<TEntity>`
- Design notes and schema diagrams tracked alongside the code (see `TEcommerceWebApi/AdvancedQuery`, `TEcommerceWebApi/NextTopics`)

**Sample endpoints**
```
GET  /api/v2/categories
GET  /api/v2/categories/{categoryId}
POST /api/v2/categories
GET  /api/v2/products
POST /api/v2/products
POST /api/v2/users
GET  /api/v2/users/{userId}
POST /api/v2/orders/checkout
GET  /api/v2/orders/{orderId}
GET  /api/v2/orders/user/{userId}
GET  /api/v2/analytics/category-summary
GET  /api/v2/analytics/top-selling-products
GET  /api/v2/analytics/customer-spending
GET  /api/v2/analytics/sales-overview
```

### 6. `OrganizationManagement` (current focus)
Where the **repository pattern is being deliberately refined** on a simpler domain (`Department`, `Designation`) so the pattern itself is the thing being practiced, not buried under e-commerce complexity.

- A shared `GenericRepository<TEntity> : IGenericRepository<TEntity>` for common CRUD
- Entity-specific repositories (`DepartmentRepository`, `DesignationRepository`) composing the generic base for custom queries
- AutoMapper-based DTOs per entity (`Create` / `Read` / `Update`)
- **Next up:** Unit of Work on top of these repositories, to coordinate multiple repository operations under a single `SaveChanges` transaction

---

## 🧭 Roadmap

What's next as the lab keeps growing:

- [x] Repository pattern (first pass in `TEcommerceWebApi`, refined in `OrganizationManagement`)
- [ ] Unit of Work
- [ ] Authentication & Authorization (JWT, Identity)
- [ ] Caching strategies (in-memory, distributed)
- [ ] Clean/Onion Architecture
- [ ] Unit & integration testing
- [ ] System design fundamentals (scalability, load balancing, caching layers, message queues)
- [ ] Docker & CI/CD deployment

---

## 🛠️ Tech Stack

- **Language:** C#
- **Runtime:** .NET 10
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Database:** PostgreSQL
- **Mapping:** AutoMapper
- **API Docs:** Swashbuckle / OpenAPI

---

## ▶️ Getting Started

Each project is a standalone .NET application. To run any of them:

```bash
cd <ProjectName>       # e.g. TEcommerceWebApi
dotnet restore
dotnet ef database update   # for Web API projects using EF Core
dotnet run
```

The full solution can also be opened via:

```bash
The-Ultimate-C_Sharp-Learning-Lab-TestLine.sln
```

---

## 📚 Notes & References

Design notes, database schema drafts, and topic learning plans are kept alongside the code they relate to (e.g. `TEcommerceWebApi/AdvancedQuery`, `MasteringOOP/MiniProject`) so context isn't lost between sessions.

### The 6 Constraints of REST API

1. **Client-Server Architecture** — separation of concerns between client and server
2. **Statelessness** — every request is independent and self-contained
3. **Cacheability** — responses must define themselves as cacheable or not
4. **Uniform Interface** — every URL/URI is unique and resource-oriented
5. **Layered System** — client cannot tell if it's connected directly to the server
6. **Code on Demand** *(optional)* — server can extend client functionality by transferring executable code

---

*This repository is a living log of my journey learning C# and .NET — from "Hello World" through backend architecture patterns (controllers, services, repositories, generic repository, Unit of Work) and, eventually, system design.*
