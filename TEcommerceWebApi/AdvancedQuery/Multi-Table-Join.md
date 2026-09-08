---

### Real-World Business Scenario
A customer or admin opens the order details page: **`GET /api/v2/orders/{orderId}`**.

We need to return:
1. **Order Info:** `OrderId`, `OrderDate`, `TotalAmount`, `Status`
2. **Customer Info:** `FullName`, `Email` (from `Users` table)
3. **List of Items:** `Quantity`, `UnitPrice` (from `OrderItems` table)
4. **Product Details for each item:** `ProductName` (from `Products` table)
5. **Category Details for each product:** `CategoryName` (from `Categories` table)

That means **1 query across 4 related tables!**

---

### Concept 1: `.Include()` vs `.ThenInclude()` (The Multi-Hop Rule)

In EF Core, how do you navigate across tables?

```
Order (Root) 
  ├──> User (1 hop)
  └──> OrderItems (1 hop) 
            └──> Product (2nd hop) 
                      └──> Category (3rd hop)
```

#### The Golden Rule:
* **`.Include()`** always starts from the **Root Entity** (`Order`).
* **`.ThenInclude()`** drills deeper into the entity you **just included**.

```csharp
var order = await _appDbContext.Orders
    .AsNoTracking()
    .Include(o => o.User)                            // Hop 1: Order -> User
    .Include(o => o.OrderItems)                      // Hop 1: Order -> OrderItems
        .ThenInclude(oi => oi.Product)               // Hop 2: OrderItem -> Product
            .ThenInclude(p => p.Category)            // Hop 3: Product -> Category
    .FirstOrDefaultAsync(o => o.OrderId == orderId);
```

#### What SQL does EF Core generate for this?
EF Core translates this into multi-table `LEFT JOIN`s:
```sql
SELECT o."OrderId", o."OrderDate", o."TotalAmount", o."Status",
       u."UserId", u."FullName", u."Email",
       oi."OrderItemId", oi."Quantity", oi."UnitPrice",
       p."ProductId", p."Name",
       c."CategoryId", c."Name"
FROM "Orders" AS o
LEFT JOIN "Users" AS u ON o."UserId" = u."UserId"
LEFT JOIN "OrderItems" AS oi ON o."OrderId" = oi."OrderId"
LEFT JOIN "Products" AS p ON oi."ProductId" = p."ProductId"
LEFT JOIN "Categories" AS c ON p."CategoryId" = c."CategoryId"
WHERE o."OrderId" = @orderId
```

---

### Concept 2: The "Cartesian Explosion" & `.AsSplitQuery()`

This is an **essential enterprise concept** every senior .NET developer must know.

#### The Problem:
In SQL, when you `LEFT JOIN` multiple collections (e.g., an Order with 10 OrderItems and 5 Shipments), SQL returns **duplicated rows** for all the parent columns.
* If an order has 20 items, the user's name and order details are repeated 20 times over the network wire. 
* This is called **Cartesian Explosion** or **Data Duplication**.

#### The EF Core Solution: `.AsSplitQuery()`

```csharp
var order = await _appDbContext.Orders
    .AsNoTracking()
    .AsSplitQuery() // ⚡ Magic line!
    .Include(o => o.User)
    .Include(o => o.OrderItems)
        .ThenInclude(oi => oi.Product)
            .ThenInclude(p => p.Category)
    .FirstOrDefaultAsync(o => o.OrderId == orderId);
```

#### What does `.AsSplitQuery()` do under the hood?
Instead of 1 massive SQL query with duplicated data, EF Core sends **2 clean, fast SQL queries**:
1. `SELECT * FROM Orders JOIN Users WHERE OrderId = @id;`
2. `SELECT * FROM OrderItems JOIN Products JOIN Categories WHERE OrderId = @id;`

EF Core then stitches them together in memory automatically. It uses less memory and runs faster when fetching collections.

---

### Concept 3: The DTOs & Zero-Overhead Projection (`.Select()`)

While `.Include()` fetches full entity objects, in production we usually map directly to **DTOs** so we only return the exact data the client needs.

#### 1. The DTOs
Create `DTOs/OrderReadDto.cs`:

```csharp
using System;
using System.Collections.Generic;

namespace TEcommerceWebApi.DTOs
{
    public class OrderReadDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;

        // Customer Info (Flattened from User)
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        // Nested Items
        public List<OrderItemReadDto> Items { get; set; } = new List<OrderItemReadDto>();
    }

    public class OrderItemReadDto
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice; // Calculated property
    }
}
```

---

#### 2. Writing the Deep Projection Query

Look how clean and readable this query is using `.Select()`:

```csharp
public async Task<OrderReadDto?> GetOrderByIdAsync(Guid orderId)
{
    return await _appDbContext.Orders
        .AsNoTracking()
        .Where(o => o.OrderId == orderId)
        .Select(o => new OrderReadDto
        {
            OrderId = o.OrderId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            Status = o.Status.ToString(),
            CustomerName = o.User != null ? o.User.FullName : string.Empty,
            CustomerEmail = o.User != null ? o.User.Email : string.Empty,
            
            // Map the child collection:
            Items = o.OrderItems.Select(oi => new OrderItemReadDto
            {
                OrderItemId = oi.OrderItemId,
                ProductId = oi.ProductId,
                ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                CategoryName = oi.Product != null && oi.Product.Category != null 
                               ? oi.Product.Category.Name 
                               : string.Empty,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        })
        .FirstOrDefaultAsync();
}
```

---

### Why Direct Projection with `.Select()` is the Best Practice:
1. **No `.Include()` needed:** EF Core inspects the properties accessed inside `Select()` and automatically creates the optimal SQL `JOIN` statements.
2. **Optimal SQL:** It fetches **only the required columns** (e.g. it skips `Description`, `PasswordHash`, `CreatedAt`, etc.).
3. **No tracking overhead:** It instantiates the DTOs directly without EF Core Change Tracker involvement.

---

### Practice Task for You

Create `OrderReadDto.cs` in your `DTOs` folder and examine the structure of:
* `o.OrderItems.Select(...)` mapping the child collection inside the parent query.