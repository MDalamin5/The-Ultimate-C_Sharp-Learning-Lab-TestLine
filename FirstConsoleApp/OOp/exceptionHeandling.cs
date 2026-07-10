namespace FirstConsoleApp.ExceptionLearning
{
    public class Person
    {
        
    }

    public class Drivers
    {
        public static void Demo()
        {
            
            Console.Write("Enter the Number: ");
            int number1 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the 2nd number: ");
            int number2 = Convert.ToInt32(Console.ReadLine());

            try
            {
                int result = number1 / number2;
                Console.WriteLine($"The result is {result}");
                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Oops! You can't divide by zero.");
                Console.WriteLine($"Error Details: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Cleanup database and files....");
                
            }
            
            
        }
    }
}