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
            
            
            
        }
    }
}