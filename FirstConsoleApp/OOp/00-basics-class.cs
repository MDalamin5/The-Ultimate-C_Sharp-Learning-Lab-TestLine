using System;
namespace FirstConsoleApp.OOp
{
    class DemoPerson
    {
        public string name;
        public int age;
    }

    public class ClassTEst{

    

    public static void PersonRun()
        {
            Console.WriteLine("Ehllo form class");
            DemoPerson p1 = new DemoPerson();
            p1.name = "Md Al Amin";
            p1.age = 32;

            Console.WriteLine($"Name: {p1.name}, Age: {p1.age}");
        }
    
    }
}
