using System;
using MasteringOOP.Interface;

namespace MasteringOOP.Interface;

public class CheckoutManager
{
    public readonly INotificationService _notificationService;

    public CheckoutManager(INotificationService NotificationService)
    {
        _notificationService = NotificationService;
    }


    public void CompletePurchase(string email, decimal amount)
    {
        Console.WriteLine("Transactions is processing....");
        _notificationService.SendReceipt(email, amount);

        Console.WriteLine("Purchase Completed. Thank you!");
        
        
    }
}