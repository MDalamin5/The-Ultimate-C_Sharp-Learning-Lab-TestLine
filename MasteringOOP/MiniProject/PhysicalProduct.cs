using System;
namespace MasteringOOP.ShoppingCartProject;

public class PhysicalProduct : Product
{
    public decimal ShippingCost {get; private set;}

    public PhysicalProduct(decimal shippingCost, string ProductName, decimal ProductPrice) : base(ProductName, ProductPrice)
    {
        ShippingCost = shippingCost;
    }
    public override void DisplayProductInfo()
    {
        throw new NotImplementedException();
    }
}