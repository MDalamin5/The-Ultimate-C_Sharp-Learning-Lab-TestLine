namespace FirstConsoleApp.ExceptionLearning
{
    public class Person
    {
        
    }

    public class Drivers
    {
        public static void Demo()
        {
            try
            {
                Console.Write("Enter the Number: ");
                // Moving input inside try to catch FormatExceptions (if user types "abc")
                int number1 = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the 2nd number: ");
                int number2 = Convert.ToInt32(Console.ReadLine());

                if (number2 > number1)
                {
                    throw new ArgumentException("Number 2 can be gater then number 1.");
                }

                int result = number1 / number2;
                Console.WriteLine($"The result is {result}");

                // Testing Null Exception
                Console.Write("Enter any sting value: ");
                string? name = Console.ReadLine(); 
                Console.WriteLine($"The length of the name is: {name!.Length}"); // Use ! to force it for the test
            }
            // 1. Catch specific math error
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: You cannot divide by zero.");
            }
            // 2. Catch specific input error (if user types letters)
            catch (FormatException)
            {
                Console.WriteLine("Error: You must enter a valid whole number.");
            }
            // 3. Catch null error
            catch (NullReferenceException)
            {
                Console.WriteLine("Error: You tried to access a property of a null object!");
            }

            // argument exceptions via threw 
            catch (ArgumentException ex)
            {
                Console.WriteLine($"The argument exceptions error: {ex.Message}");
                
            }
            // 4. Catch anything else
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("System cleanup complete.");
            }
        }
    }
}