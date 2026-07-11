using System;

namespace FirstConsoleApp.Basics
{
    public class Arrays
    {
        public static void Run()
        {
            Console.WriteLine("--- Array Module ---");
            string[] fruits = ["Apple", "Banana", "Orange"];
            
            // print the array
            for(int i=0; i<3; i++)
            {
                Console.WriteLine(fruits[i]);
                
            }
        }
    }
}