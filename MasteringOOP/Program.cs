using System;
using MasteringOOP.Inheritance;

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
                fullName: "John Doe", 
                email: "john.doe@example.com", 
                createdBy: "SystemAdmin"
            );

            Console.WriteLine(customer.ToString());
        }
    }
}