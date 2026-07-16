using System;

namespace MasteringOOP.Polymorphism;

public abstract class PaymentProcessor
{
    // A virtual method has default logic, but CAN be changed by a child.
    public virtual void ValidateTransaction(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }
        Console.WriteLine("Standard validation passed.");
    }

    // An abstract method has NO logic. The child MUST provide it.
    public abstract void ProcessPayment(decimal amount);
}