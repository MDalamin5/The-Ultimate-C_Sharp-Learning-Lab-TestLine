using System;
using System.Data;
namespace MasteringOOP.ShoppingCartProject;

public class ShoppingCart
{
    private readonly ITaxService _taxService;

    private List<Product> _items = new List<Product>();
    public ShoppingCart(ITaxService taxService)
    {
        _taxService = taxService;
        // _items.Add(item);
    }

    public void AddItem(Product product)
    {
        if(product == null)
        {
            throw new InvalidCartItemException("Failed to add item: The product provided does not exist or is null.");
        }

        _items.Add(product);
    }

    public void CheckOut()
    {
        decimal ProductPriceSum=0.0m;

        foreach(Product product in _items)
        {
            product.DisplayProductInfo();
            
        }

        ProductPriceSum = _items.Sum(p => p.Price);

        Console.WriteLine($"Total amount of your Product is: {ProductPriceSum}");

        decimal totalTax = _taxService.CalculateTax(ProductPriceSum);

        Console.WriteLine($"Tax of your product is: {totalTax}");
        Console.WriteLine($"Tax: {totalTax}, So Grand Total is: {totalTax + ProductPriceSum}");
        
    }
}