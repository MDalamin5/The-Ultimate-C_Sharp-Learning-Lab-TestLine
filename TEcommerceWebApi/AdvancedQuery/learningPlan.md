
### The Complete Domain Model Plan (E-Commerce Schema)

```
       ┌──────────────────┐
       │   UserProfile    │
       └────────▲─────────┘
                │ 1:1
       ┌────────┴─────────┐              1:M       ┌──────────────────┐
       │       User       ├───────────────────────>│      Review      │
       └────────┬─────────┘                        └────────▲─────────┘
                │ 1:M                                       │ M:1
                ▼                                           │
       ┌──────────────────┐        1:M             ┌────────┴─────────┐
       │      Order       ├───────────────────────>│     Product      │
       └────────┬─────────┘                        └────────▲─────────┘
                │ 1:M                                       │ 1:M
                ▼                                           │
       ┌──────────────────┐               M:1               │
       │    OrderItem     ├─────────────────────────────────┘
       └──────────────────┘
                │ M:1
       ┌────────┴─────────┐
       │     Category     │
       └──────────────────┘
```

---

### The 4 Practical Modules to Master

#### Module 1: The Many-to-Many Domain Model (`Order` $\leftrightarrow$ `OrderItem` $\leftrightarrow$ `Product`)
* **What you will build:** `User`, `Order`, and `OrderItem` models.
* **EF Core skills:** 
  * Composite foreign keys and explicit junction tables with payloads (`Quantity`, `UnitPrice`).
  * Configuring `DeleteBehavior.Cascade` vs `DeleteBehavior.Restrict` in complex dependency trees.
  * Writing migrations without data loss.

---

#### Module 2: Deep Multi-Table Joins (`Include` vs `ThenInclude` vs Projections)
* **What you will query:** 
  * *"Get user order history with items, product names, category names, and images."*
* **EF Core skills:**
  * `.Include().ThenInclude()` (Multi-level `LEFT JOIN` in SQL).
  * **AsSplitQuery()**: Understanding when a single massive SQL query with multiple `LEFT JOIN`s causes *Cartesian Explosion* and how EF Core splits it into multiple fast SQL queries.
  * Nested DTO Projections: Writing zero-overhead queries using `.Select()` across 4 tables simultaneously.

---

#### Module 3: Aggregations, Grouping & Analytical Queries (Dashboard APIs)
* **What you will query:**
  1. **Top-Selling Products:** Products sorted by total units sold (`SUM(OrderItem.Quantity)`).
  2. **Category Analytics:** Category name, total product count, average price, and total revenue.
  3. **User Spend Report:** Total money spent by each user with their order count.
* **EF Core skills:**
  * `.GroupBy()` translation into SQL `GROUP BY`.
  * `.Count()`, `.Sum()`, `.Average()`, `.Min()`, `.Max()`.
  * Filtering groups using `.Where()` (SQL `HAVING` clause equivalent).

---

#### Module 4: Atomic Transactions & Business Logic (The "Place Order" API)
* **What you will build:** A complete `CheckoutOrder(CreateOrderDto)` endpoint.
* **EF Core skills:**
  * **Transactions (`IDbContextTransaction`):** Ensuring that stock deduction + order creation + order items insertion succeed together or rollback completely if anything fails.
  * **Concurrency & Stock Checking:** Validating product availability in real-time before checkout.
  * Creating parent and child entities in a single `SaveChangesAsync()` call (EF Core Unit of Work).

---

#### Module 5: Enterprise Patterns (Soft Deletes & Global Query Filters)
* **What you will build:** An automatic `IsDeleted` system.
* **EF Core skills:**
  * `modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted)`.
  * Understanding how EF Core automatically attaches `WHERE "IsDeleted" = false` to every single query in the application without having to write it manually each time.
  * Bypassing filters when needed using `.IgnoreQueryFilters()`.

---
