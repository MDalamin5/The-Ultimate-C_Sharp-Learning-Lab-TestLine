using System;
namespace FirstConsoleApp.Basics
{
    public class DigitChecks
    {
        public static void Digit()
        {
            Console.Write("Enter your Number: ");

            int userInput = Convert.ToInt32(Console.ReadLine());
            if(userInput % 2 == 0)
            {
                Console.WriteLine($"{userInput} is the Even number.");
            }
            else
                Console.WriteLine($"{userInput} is Odd Number.");
        }
    }
}