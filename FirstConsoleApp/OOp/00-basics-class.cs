using System;
namespace FirstConsoleApp.OOp
{
    class DemoPerson
    {
        public string? name;
        public int age;

        public DemoPerson()
        {
            Console.WriteLine("Its a blank Constructor...");
        }

        // creating constructor
        public DemoPerson(string name, int age)
        {
            Console.WriteLine("We are in constructor...");
            this.name = name;
            this.age = age;    
        }


        public void setVaue(string n, int a)
        {
            name = n;
            age = a;
        }

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
            p1.setVaue("Md Al Amin", 23);
            DemoPerson p2 = new DemoPerson {name = "Miad", age = 33};

            Console.WriteLine($"Name: {p1.name}, Age: {p1.age}");
            p2.DisplayInfos();

            DemoPerson p3 = new DemoPerson("Arif", 34);
            p3.DisplayInfos(); 
        }
    
    }
}
