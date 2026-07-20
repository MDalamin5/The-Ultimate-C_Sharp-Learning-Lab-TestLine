using System;
namespace MasteringOOP.ShoppingCartProject;

public class BdTaxService : ITaxService
{
    public decimal CalculateTax(decimal totalAmount)
    {
        decimal taxAmount =  totalAmount * 0.15m;
        return taxAmount;
    }
}