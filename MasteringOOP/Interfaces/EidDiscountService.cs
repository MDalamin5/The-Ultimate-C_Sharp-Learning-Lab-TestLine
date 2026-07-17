using System;
namespace MasteringOOP.Interface;

public class EidDiscountService : IDiscountService
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount * .20m;
    }
}