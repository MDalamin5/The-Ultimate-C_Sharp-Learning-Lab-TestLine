using System;
namespace MasteringOOP.ShoppingCartProject;

public class TaxFreeService : ITaxService
{
    public decimal CalculateTax(decimal totalAmount)
    {
        decimal taxAmount =  totalAmount * 0.0m;
        return totalAmount + taxAmount;
    }
}