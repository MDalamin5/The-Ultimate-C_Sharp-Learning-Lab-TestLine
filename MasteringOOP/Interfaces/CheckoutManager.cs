using System;
using MasteringOOP.Interface;

namespace MasteringOOP.Interface;

public class CheckoutManager
{
    public readonly INotificationService _notificationService;
    public readonly IDiscountService _discountService;

    public CheckoutManager(INotificationService NotificationService, IDiscountService DiscountService)
    {
        _notificationService = NotificationService;
        _discountService = DiscountService;
    }


    public void CompletePurchase(string email, decimal amount)
    {
        Console.WriteLine("Transactions is processing....");
        decimal afterDiscount = _discountService.ApplyDiscount(amount);

        _notificationService.SendReceipt(email, afterDiscount);

        Console.WriteLine($"Your Discount amount is : {amount - afterDiscount}");
        
        Console.WriteLine("Purchase Completed. Thank you!");
        
        
    }
}