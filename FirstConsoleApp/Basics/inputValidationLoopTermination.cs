using System;
using System.Reflection.Metadata;
namespace FirstConsoleApp.Basics
{
    public class LoopTermination
    {
        public static void LoopTer()
        {
            while (true)
            {
                Console.Write("Enter the number which is 1 to 10: ");

                string input = Console.ReadLine() ?? "";
                input = input.ToLower().Trim();
                if (input == "exit")
                {
                    Console.WriteLine("You press Exit so Good Bye...");
                    break;
                }

                if (!int.TryParse(input, out int number))
                {
                    Console.WriteLine("Please enter a valid number");
                    continue;
                }

                if (!(number >= 0 && number <= 5))
                {
                    Console.WriteLine("The number is not in the range..");
                    continue;
                }
                 int add = number + number;
                    Console.WriteLine($"Sum is: {add}");

            }
            

            
        }
    }
}