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
            throw new NoNullAllowedException("You can no call the function without product.");
        }

        _items.Add(product);



    }

    public void CheckOut()
    {
        decimal ProductPriceSum=0.0m;

        foreach(Product product in _items)
        {
            product.DisplayProductInfo();
            ProductPriceSum += ProductPriceSum;
        }
        Console.WriteLine($"Total amount of your Product is: {ProductPriceSum}");
        
        
    }
}