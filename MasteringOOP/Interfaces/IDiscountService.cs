using System;
namespace MasteringOOP.Interface;

public interface IDiscountService
{
    decimal ApplyDiscount(decimal originalPrice);
}