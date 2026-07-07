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
            
        }
    }
}