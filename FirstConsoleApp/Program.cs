using System.Net.Security;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

// class Test
// {
//     void display(string name)
//     {
//         Console.WriteLine($"ITs call from other functions...{name}");
//         for(int i=0; i<5; i++)
//         {
//             // Console.WriteLine(i);
//         }
//     }
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("test line");
//         Test obj = new Test();
//         obj.display("hello");
//         obj.display("hi");
//     }
// }

// class Program
// {
//     public struct myValue(int x, int y)
//     {
//         public int X {get; init;} = x;
//         public int Y {get; set;} = y;
//     }
//     public static void Main(string[] args)
//     {
//         Dog myDog = new Dog("Tommy");
//         myDog.Bark();

//         char firstChat = 'c';
//         var limit = 3;
//         Console.WriteLine(firstChat);
//         int[] source = [0, 1, 3, 4, 5, 6];
//         var query = from item in source where item <= limit select item;

//         foreach(var item in query)
//         {
//             Console.WriteLine(item);
//         }

//         // Structure type vale
//         myValue data = new myValue(2, 3);
//         Console.WriteLine(data.X);
//         data.Y = 10;
//         Console.WriteLine(data.Y);
        
        
//     }
// }

public record Person(string FirstName, string LastName)
{
    public required string[] PhoneNumbers { get; init; }
}

public class Program
{
    public static void Main()
    {
        Person person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
        Console.WriteLine(person1);
        // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

        Person person2 = person1 with { FirstName = "John" };
        Console.WriteLine(person2);
        // output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
        Console.WriteLine(person1 == person2); // output: False

        person2 = person1 with { PhoneNumbers = new string[1] };
        Console.WriteLine(person2);
        // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }
        Console.WriteLine(person1 == person2); // output: False

        person2 = person1 with { };
        Console.WriteLine(person1 == person2); // output: True
    }
}