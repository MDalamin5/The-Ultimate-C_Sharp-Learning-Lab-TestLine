using System;

namespace FirstConsoleApp.Basics    
{
    public class SuperBasic
    {
        public static void Basic()
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

        SuperBasic ownObj = new SuperBasic();

        ownObj.demoFunction(userName);

        // Multiple variable
        int num1, num2, num3;
        num1=num2=num3=10;

        Console.WriteLine($"The number is {num1}, {num2}, {num3}");


        // datatype conversations use Convert functions
        int salary = 4000;
        Console.WriteLine(Convert.ToString(salary));
        Console.WriteLine(Convert.ToString(salary).GetType());

        //data-conversations
        string data = "10";
        bool ssuccess = int.TryParse(data, out int result);
        Console.WriteLine($"This string value is: {data}, and convert success: {ssuccess}, and th result is: {result}");

        // take input from user
        Console.Write("Can you please enter your name: ");
        string? inputName = Console.ReadLine();
        // Console.WriteLine($"Your name is: {inputName}");

        // take input integer data
        int personAge;
        Console.Write("Enter your age: ");
        personAge = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Got your data...");
        Console.WriteLine($"Your name is: {inputName}, Age: {personAge}");

        // take double input
        double inputDouble;
        Console.WriteLine("Enter a Double number: ");

        inputDouble = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"Your name is: {inputName}, Age: {personAge}, your GPA: {inputDouble}");

        //ternary operator.

        int resultt = 5 >= 4 ? 9: 6;
        Console.WriteLine(resultt);
        }

         void demoFunction(string Uname)
    {
        
        Console.WriteLine($"The name is: {Uname}");
    }
    }
}