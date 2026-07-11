using System.Globalization;
using System.Linq;
namespace FirstConsoleApp.Basics;
public class ArrayADV
{
    public static void Run()
    {
        int[] num = [1, 3, 2, 11, 56, 22, 67, 98, 8, 9, 10];

        var bigNumbers = num.Where(val => val > 10).ToArray();
        var sorted = num.OrderBy(n => n).ToArray();

        foreach(var bignumber in bigNumbers)
        {
            Console.Write($"{bignumber} ");
            
        }
        Console.WriteLine("\nSorted list.");

        foreach(var data in sorted)
        {
            Console.Write($"{data} ");
            
        }
        Console.WriteLine();

        Console.WriteLine($"Max: {num.Max()}");
        Console.WriteLine($"Min: {num.Min()}");
        Console.WriteLine($"AVg: {num.Average()}");

        // existence check
        Console.WriteLine($"Is present: {num.Contains(33)}");
        
        
        
        
    }
}