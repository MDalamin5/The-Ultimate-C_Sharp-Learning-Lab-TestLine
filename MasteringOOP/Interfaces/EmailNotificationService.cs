using System;

namespace MasteringOOP.Interface;

public class EmailNotificationService : INotificationService
{
    public void SendReceipt(string customerEmail, decimal amount)
    {
        Console.WriteLine($"Email send Success: {customerEmail} with amount: {amount}");
        
    }
}