using System.Collections.Generic;
namespace GenericCsharp
{
    public class LiNQPractice
    {

        public class Student
        {
            public string? Name {get; set;}
            public int Age {get; set;}
        }
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

            
            Console.WriteLine("===========Advanced Level=============");
            
            
            List<Student> totalStudents = [
                new Student {Name = "Al Amin", Age = 25},
                new Student {Name = "Miad", Age = 42},
                new Student {Name = "Nirob", Age = 65}
            ];
            
            Student? miad = totalStudents.Find(s => s.Name == "Miad");

            List<Student> olderStudents = totalStudents.FindAll(s => s.Age > 30);

            totalStudents.Sort((a, b) => a.Age.CompareTo(b.Age));

            bool hasHasan = totalStudents.Exists(s => s.Name == "Hasan");

            foreach(Student stu in olderStudents)
            {
                Console.WriteLine($"Name: {stu.Name}, Age: {stu.Age}");
                
            }


            
        }
    }
}