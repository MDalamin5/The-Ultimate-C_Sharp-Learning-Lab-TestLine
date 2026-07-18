using System;
using System.Globalization;
namespace MasteringOOP.ShoppingCartProject;

public abstract class Product
{
    public Guid Id {get; private set;}
    public string? Name {get; private set;}
    public decimal Price{get; private set;}

    public Product(string ProductName, decimal ProductPrice)
    {
        Id = Guid.NewGuid();
        Name = ProductName;
        Price = ProductPrice;
    }

    public abstract void DisplayProductInfo();
}