using System;
namespace FirstConsoleApp.OOp
{
    class DemoPerson
    {
        public string? name;
        public int age;

        public void DisplayInfos()
        {
            Console.WriteLine($"Name: {name}, Age: {age}");
        }
    }

    public class ClassTEst{

    

    public static void PersonRun()
        {
            Console.WriteLine("Ehllo form class");
            DemoPerson p1 = new DemoPerson();
            p1.name = "Md Al Amin";
            p1.age = 32;
            DemoPerson p2 = new DemoPerson {name = "Miad", age = 33};

            Console.WriteLine($"Name: {p1.name}, Age: {p1.age}");
            p2.DisplayInfos();
        }
    
    }
}
