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
                // 1. Get inputs through a clean helper method
                int number1 = GetIntFromUser("Enter the 1st number: ");
                int number2 = GetIntFromUser("Enter the 2nd number: ");

                // 2. Perform logic in a separate method
                int result = CalculateDivision(number1, number2);
                Console.WriteLine($"The result is {result}");

                // 3. String test
                HandleStringLengthTest();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Math Error: Cannot divide by zero.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("System cleanup complete.");
            }
        }

        // HELPER: Handles input and validation (No more messy Convert.ToInt32 in Main)
        private static int GetIntFromUser(string prompt)
        {
            while (true) // Keep asking until they give a valid number
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";

                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                
                Console.WriteLine("Invalid input! Please enter a whole number.");
            }
        }

        // LOGIC: Handles the "Business Rules" and Throws exceptions
        private static int CalculateDivision(int n1, int n2)
        {
            if (n2 > n1)
            {
                throw new ArgumentException("The second number cannot be greater than the first.");
            }
            return n1 / n2;
        }

        // TEST: Keeps the main flow clean
        private static void HandleStringLengthTest()
        {
            Console.Write("Enter a string to check length: ");
            string? name = Console.ReadLine();
            
            if (name == null) throw new NullReferenceException();
            
            Console.WriteLine($"Length: {name.Length}");
        }
    }

}