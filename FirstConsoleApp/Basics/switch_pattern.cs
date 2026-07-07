using System;
namespace FirstConsoleApp.Basics
{
    public class SwitchPattern
    {
        public static void switchs()
        {
            object number = 5;

            string result = number switch
            {
                int => "Integer",
                double => "Double",
                string => "string",
                _ => "Unkonwn Type"
            };

            Console.WriteLine($"The type is {result}");

            // Conditions with the switch-case
            switch (number)
            {
                case int num when num % 2 == 0:
                    Console.WriteLine("Even number");
                    break;
                default:
                    Console.WriteLine("Odd number..");
                    break;
            }
            
        }
    }
}