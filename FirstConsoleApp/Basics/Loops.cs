using System;

namespace FirstConsoleApp.Basics // Notice: ProjectName.FolderName

{
    public class Loops
    {
        public static void Run()
        {
            Console.WriteLine("--- Loop Module ---");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Loop index: {i}");
            }
        }
    }
}