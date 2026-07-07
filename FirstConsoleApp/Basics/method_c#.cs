using System;
namespace FirstConsoleApp.Basics
{
    public class MethodPractice
    {
        public  int Calculation(int num1, int num2, string operations)
        {
            if(operations == "Add")
            {
                return num1 + num2;
            }
            else if(operations == "Sub")
            {
                return num1 - num2;
            }
            else
                return 0;
        }
        public static void funcTest()

        { 
            MethodPractice obj = new MethodPractice();
            int result =  obj.Calculation(7, 10, "Sub");
            Console.WriteLine(result);
        }
    }
}