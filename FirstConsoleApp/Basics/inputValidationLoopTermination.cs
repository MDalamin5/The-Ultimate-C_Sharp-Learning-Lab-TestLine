using System;
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
                input = input.ToLower();
                if (input == "exit")
                {
                    Console.WriteLine("You press Exit so Good Bye...");
                    break;
                }
            }

            
        }
    }
}