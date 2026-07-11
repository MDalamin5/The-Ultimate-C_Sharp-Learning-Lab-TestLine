using System;

namespace FirstConsoleApp.Basics
{
    public class Arrays
    {
        public static void Run()
        {
            Console.WriteLine("--- Array Module ---");
            // array inti syntax opt1
            // string[] fruits = new string[10];

            //opt2
            string[] fruits = {"Mango", "yellow-banana", "black-banana"};

            //opt3
            // string[] fruits = ["Apple", "Orange", "Banana"];
            
            // print the array using vanilla for loop
            for(int i=0; i<fruits.Length; i++)
            {
                Console.WriteLine(fruits[i]);
                
            }

            Console.WriteLine("Printing array via the ForEachLoop");
            foreach(string fruit in fruits)
            {
                Console.WriteLine($"{fruit}");
                
            }
            
        }
    }
}