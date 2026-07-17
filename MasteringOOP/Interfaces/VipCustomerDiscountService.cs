using System;
namespace MasteringOOP.Interface;

public class VipCustomerDiscountService : IDiscountService
{
    public decimal ApplyDiscount(decimal amount)
    {
        return amount / 2;
    }
}