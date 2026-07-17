using System;
using MasteringOOP.Interface;

namespace MasteringOOP.Interfaces;

public class SmsNotificationService : INotificationService
{
    public void SendReceipt(string customerEmail, decimal amount)
    {
        // In a real app, this would use Twilio
        // Note: We might look up the phone number via email in a real database.
        Console.WriteLine($"[SMS] Texting receipt for ${amount} to user associated with {customerEmail}.");
    }
}