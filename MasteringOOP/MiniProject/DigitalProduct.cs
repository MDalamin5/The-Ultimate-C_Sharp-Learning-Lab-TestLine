using System;
namespace MasteringOOP.ShoppingCartProject;

public class DigitalProduct: Product
{
    public string DownloadLink {get; private set;}

    public DigitalProduct(string downloadLink, string ProductName, decimal ProductPrice) : base(ProductName, ProductPrice)
    {
        DownloadLink = downloadLink;
    }

    public override void DisplayProductInfo()
    {
        Console.WriteLine($"Product Name: {Name}, Product Price: {Price}, DownloadLink: {DownloadLink}");
    }
}