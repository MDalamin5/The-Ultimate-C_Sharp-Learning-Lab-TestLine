# 📋 Sprint Ticket: Customer Analytics & Order History

You will build **two new endpoints**:

---

## 🎯 Task 1: High-Value Customer Summary (VIP Report)

### 1. Goal
The marketing team needs a dashboard endpoint to see high-value customers:
* **Endpoint:** `GET /api/v2/analytics/customer-spending`
* **Query Parameter:** `[FromQuery] decimal minSpent = 0` (filters out customers who spent less than this amount).

### 2. Required Response DTO (`CustomerSpendingDto`)
Create a DTO with the following fields:
* `UserId` (Guid)
* `CustomerName` (string)
* `Email` (string)
* `TotalOrdersPlaced` (int — count of orders)
* `TotalAmountSpent` (decimal — sum of `TotalAmount` from all orders)
* `LastOrderDate` (DateTime? — date of the most recent order, or null if no orders)

### 3. Business Rules for the Query
1. Query from `Users` and join/aggregate their `Orders`.
2. Filter by `minSpent` (e.g. only return users where `TotalAmountSpent >= minSpent`).
3. Order the results by `TotalAmountSpent` descending (biggest spenders at the top).
4. Use `.AsNoTracking()`.

> 💡 **SQL Equivalent Hint for Task 1:**
> ```sql
> SELECT u."UserId", u."FullName", u."Email",
>        COUNT(o."OrderId") AS "TotalOrdersPlaced",
>        COALESCE(SUM(o."TotalAmount"), 0) AS "TotalAmountSpent",
>        MAX(o."OrderDate") AS "LastOrderDate"
> FROM "Users" u
> LEFT JOIN "Orders" o ON u."UserId" = o."UserId"
> GROUP BY u."UserId", u."FullName", u."Email"
> HAVING COALESCE(SUM(o."TotalAmount"), 0) >= @minSpent
> ORDER BY "TotalAmountSpent" DESC;
> ```

---

## 🎯 Task 2: Get Customer Order History with Status Filter

### 1. Goal
When a customer opens their order history page, fetch all their past orders with full line items:
* **Endpoint:** `GET /api/v2/orders/user/{userId}`
* **Query Parameter:** `[FromQuery] OrderStatus? status` (Optional: filter orders by status, e.g. `Completed` or `Pending`).

### 2. Business Rules for the Query
1. First, check if the `User` exists in the database. If not, return `404 Not Found`.
2. If `status` is provided in the query string, filter by that status (`o.Status == status`).
3. If `status` is `null`, return all orders regardless of status.
4. Order results by `OrderDate` **Descending** (newest orders first).
5. Map directly to your `OrderReadDto` (including the nested list of `OrderItemReadDto` with Product Name and Category Name).

---

## 🛠️ Step-by-Step Checklist for You

1. [ ] **Step 1:** Create `CustomerSpendingDto.cs` in the `DTOs/` folder.
2. [ ] **Step 2:** Add `Task<List<CustomerSpendingDto>> GetCustomerSpendingSummaryAsync(decimal minSpent = 0);` to `IAnalyticsService` and implement it in `AnalyticsService`.
3. [ ] **Step 3:** Add the endpoint to `AnalyticsController.cs`.
4. [ ] **Step 4:** Create `IOrderService` and `OrderService` for Task 2.
5. [ ] **Step 5:** Create `OrderController.cs` and implement `GetUserOrders(Guid userId, OrderStatus? status)`.
6. [ ] **Step 6:** Register `IOrderService` in `Program.cs` (`AddScoped`).

---

### 🚀 How to Test Your Work

1. Add some dummy users, products, orders, and order items into your database.
2. Test **Task 1** with:
   * `GET /api/v2/analytics/customer-spending`
   * `GET /api/v2/analytics/customer-spending?minSpent=100`
3. Test **Task 2** with:
   * `GET /api/v2/orders/user/{valid-user-id}`
   * `GET /api/v2/orders/user/{valid-user-id}?status=Completed`
   * `GET /api/v2/orders/user/{random-guid}` (should return 404).

