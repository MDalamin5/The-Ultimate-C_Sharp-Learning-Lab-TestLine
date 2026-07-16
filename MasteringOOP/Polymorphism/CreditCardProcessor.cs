using System;

namespace MasteringOOP.Polymorphism;

public class CreditCardProcessor : PaymentProcessor
{
    private string _cardNumber;

    public CreditCardProcessor(string cardNumber)
    {
        _cardNumber = cardNumber;
    }

    // We MUST use the 'override' keyword to fulfill the abstract contract.
    public override void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Charging ${amount} to Credit Card ending in {_cardNumber.Substring(_cardNumber.Length - 4)}");
        Console.WriteLine("Connecting to Stripe API... Success!");
    }
}