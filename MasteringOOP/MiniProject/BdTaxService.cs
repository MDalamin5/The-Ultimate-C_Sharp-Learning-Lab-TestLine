using System;
namespace MasteringOOP.ShoppingCartProject;

public class BdTaxService : ITaxService
{
    public decimal CalculateTax(decimal totalAmount)
    {
        return totalAmount * 0.15m;
    }
}