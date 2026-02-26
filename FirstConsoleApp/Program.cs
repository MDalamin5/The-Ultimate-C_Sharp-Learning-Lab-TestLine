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
    }
}