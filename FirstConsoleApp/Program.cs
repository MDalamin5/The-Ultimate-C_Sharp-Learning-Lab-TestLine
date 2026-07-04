using System.Net.Security;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

class Test
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Assalamualikum...");
        /*
            there are different type of variable
            int, string, bool, double, object type

        */
        string userName = "Md Al Amin";
        char helloChar = 'c';
        Console.WriteLine(userName);
        Console.WriteLine(12.5);
        Console.WriteLine($"Try to print the char {helloChar}");

        // bool data type
        bool isDelete = true;
        if (isDelete)
        {
            Console.WriteLine($"Is-Delete is True:  {userName}");
        }
        else
            Console.WriteLine("isDelete is False...");

        Test ownObj = new Test();

        ownObj.demoFunction(userName);
    }

    void demoFunction(string Uname)
    {
        
        Console.WriteLine($"The name is: {Uname}");
    }
}

