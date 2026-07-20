### 🚀 Phase 4: The Final OOP Mini-Project

It is time to prove you have mastered the 4 Pillars of OOP (Encapsulation, Inheritance, Polymorphism, and Abstraction/Interfaces), plus LINQ and Exception Handling.

You are going to build the core engine of an **E-Commerce Shopping Cart** entirely inside your `MiniProject` folder. 

Here are your exact requirements. Take your time, build it file by file, and put everything you've learned into practice.

#### Step 1: Inheritance & Polymorphism (The Entities)
1.  Create an `abstract class` named **`Product`**.
    *   Give it encapsulated properties: `Guid Id`, `string Name`, and `decimal Price`.
    *   Create a constructor to set `Name` and `Price` (and auto-generate the `Id`).
    *   Create an `abstract void DisplayProductInfo();` method.
2.  Create **`PhysicalProduct`** which inherits `Product`.
    *   Add a specific property: `decimal ShippingCost`.
    *   Override `DisplayProductInfo()` to print the Name, Price, and Shipping Cost.
3.  Create **`DigitalProduct`** which inherits `Product`.
    *   Add a specific property: `string DownloadLink`.
    *   Override `DisplayProductInfo()` to print the Name, Price, and Download Link.

#### Step 2: Interfaces & Dependency Injection (The Contract)
1.  Create an interface named **`ITaxService`**.
    *   It should have one method: `decimal CalculateTax(decimal totalAmount);`
2.  Create two implementations of this interface:
    *   `BdTaxService` (Returns 15% of the total amount as tax).
    *   `TaxFreeService` (Returns 0).

#### Step 3: Encapsulation & LINQ (The Engine)
1.  Create a class named **`ShoppingCart`**.
    *   It needs a `private readonly ITaxService _taxService;` (Constructor Injection!).
    *   It needs a private list to hold the items: `private List<Product> _items = new List<Product>();`
    *   Create a method `public void AddItem(Product product)` to add items to the list. (*Bonus: Throw a custom exception if the product is null*).
    *   Create a method `public void Checkout()`. 
        *   In this method, use a `foreach` loop to call `DisplayProductInfo()` on every item.
        *   Use **LINQ** to sum up the `Price` of all items in the list.
        *   Use the injected `_taxService` to calculate the tax on that sum.
        *   Print out the Subtotal, the Tax, and the Grand Total.

#### Step 4: The Grand Finale (`Program.cs`)
1.  In `Program.cs`, clear out the old code.
2.  Instantiate a Tax Service (choose either one).
3.  Instantiate the `ShoppingCart`, passing in your tax service.
4.  Create a few `PhysicalProduct` and `DigitalProduct` objects.
5.  Add them to the cart using `AddItem()`.
6.  Call `Checkout()`.
