using System;
namespace MasteringOOP.ShoppingCartProject;

public interface ITaxService
{
    decimal CalculateTax(decimal totalAmount);
}