using System.Reflection.Metadata;

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

class Program
{
    public static void Main(string[] args)
    {
        Dog myDog = new Dog("Tommy");
        myDog.Bark();

        char firstChat = 'c';
        var limit = 3;
        Console.WriteLine(firstChat);
        int[] source = [0, 1, 3, 4, 5, 6];
        var query = from item in source where item <= limit select item;

        foreach(var item in query)
        {
            Console.WriteLine(item);
        }
    }
}