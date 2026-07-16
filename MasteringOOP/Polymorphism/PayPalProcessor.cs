using System;
namespace MasteringOOP.Polymorphism
{
    public class PayPalProcessor : PaymentProcessor
    {
        private string _emailAddress;
        public PayPalProcessor(string emailAddress)
        {
            _emailAddress = emailAddress;
        }

        public override void ValidateTransaction(decimal amount)
        {
            base.ValidateTransaction(amount);

            if (!_emailAddress.Contains("@"))
            {
                throw new ArgumentException("Invalid paypal address");
            }

            Console.WriteLine("Paypal Validation process passed.");
            
        }

        public override void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Redirecting to PayPal for account {_emailAddress}... Charged ${amount}");
        }
    }
}