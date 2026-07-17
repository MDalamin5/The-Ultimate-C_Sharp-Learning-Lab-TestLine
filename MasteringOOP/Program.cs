using System;
using MasteringOOP.Inheritance;
using MasteringOOP.Polymorphism;
using MasteringOOP.Interface;
using MasteringOOP.Interfaces;

namespace MasteringOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Inheritance Testing ---");

            // We instantiate the Child class.
            // It automatically runs the Parent's constructor first!
            CustomerEntity customer = new CustomerEntity(
                fullName: "Md Al Amin", 
                email: "hi@docsqa.com", 
                createdBy: "SystemAdmin"
            );

            OrderEntity order1 = new OrderEntity(
                totalAmount: 2400,
                orderStatus: "Pending",
                createdBy: "Md Al amin"
            );

            Console.WriteLine(customer.ToString());
            Console.WriteLine(order1.ToString());



            Console.WriteLine("--- Polymorphism Testing ---");

            // Look! The type is PaymentProcessor, but the objects are specific types!
            List<PaymentProcessor> processors = new List<PaymentProcessor>
            {
                new CreditCardProcessor("4111222233334444"),
                new PayPalProcessor("hi@byvbd.com"),
                new CryptoProcessor("0x1A2B3C4D")
            };

            foreach (var processor in processors)
            {
                try
                {
                    Console.WriteLine("\n--- Processing Transaction ---");
                    processor.ValidateTransaction(150.00m); // Behaves differently based on object!
                    processor.ProcessPayment(150.00m);      // Behaves differently based on object!
                }
                catch(Exception ex)
                {
                   Console.WriteLine($"The error: {ex.Message}");
                    
                }
            }

            Console.WriteLine("\n\n--- Interfaces & Dependency Injection ---");

            INotificationService myNotificationService = new EmailNotificationService();
            INotificationService myNotificationServicePh = new SmsNotificationService();

            IDiscountService myEidDiscount = new EidDiscountService();
            IDiscountService vipDiscount = new VipCustomerDiscountService();

            CheckoutManager checkout = new CheckoutManager(myNotificationService, myEidDiscount);
            CheckoutManager checkoutPh = new CheckoutManager(myNotificationServicePh, vipDiscount);

            checkout.CompletePurchase("hi@docsqa.com", 500.00m);
            checkout.CompletePurchase("hi@byvbd.com", 4342.33m);
        }
    }
}