using System.Collections.Generic;
namespace GenericCsharp
{
    public class LiNQPractice
    {
        public static void Run()
        {
            List<string> names = ["Alamin", "miad"];

            // add more name
            names.Add("Nirob");
            names.Add("Muzahid");

            // adding multiple item in once
            string[] newStudents = ["Sakib", "Tamim"];
            names.AddRange(newStudents);

            foreach(string name in names)
            {
                Console.Write($"{name}, ");
                
            }

            Console.WriteLine();

            // remove item from the list
            names.Remove("Nirob");
            names.RemoveAt(0);

            Console.WriteLine("After removing");

            foreach(string name in names)
            {
                Console.Write($"{name}, ");
                
            }
            
            Console.WriteLine($"Total student have: {names.Count}");


            Console.WriteLine("========Moderate Level===========");
            List<int> numbers = new List<int>();
            Console.WriteLine($"Capacity: {numbers.Capacity}, Count: {numbers.Count}");


            numbers.Add(1);
            Console.WriteLine($"Capacity: {numbers.Capacity}, Count: {numbers.Count}"); // 4, 1 (It started an array of 4)

            numbers.Add(2);
            numbers.Add(3);
            numbers.Add(4);
            numbers.Add(5); // Backpack is full! It doubles the capacity to 8.
            Console.WriteLine($"Capacity: {numbers.Capacity}, Count: {numbers.Count}"); 

            
            
            
            
        }
    }
}