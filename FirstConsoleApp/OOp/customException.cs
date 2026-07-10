namespace FirstConsoleApp.CustomException
{
    public class MyCustomException : Exception
    {
        // Constructor No. 3: Takes a message AND the original "inner" error
        public MyCustomException(string message, Exception inner) 
            : base(message, inner) { }
    }


    public class DatabaseService
    {
        public static void Connect()
        {
            try
            {
                // Simulate a system error (Divide by zero)
                int zero = 0;
                int result = 10 / zero; 
            }
            catch (DivideByZeroException systemError)
            {
                // WE WRAP IT HERE
                // We tell the user "Connection Failed", 
                // but we attach the 'systemError' inside so we don't lose the technical details.
                throw new MyCustomException("Database Connection Failed!", systemError);
            }
        }
    }

    public class Program
    {
        public static void run()
        {
            try
            {
                // Calling the logic layer
                DatabaseService.Connect();
            }
            catch (MyCustomException ex)
            {
                // 1. Show the "Friendly" message to the user
                Console.WriteLine($"USER MESSAGE: {ex.Message}");

                // 2. Show the "Technical" message to the developer (The Inner Exception)
                // This is possible because of Constructor No. 3!
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"TECHNICAL DETAIL: {ex.InnerException.Message}");
                }
            }
        }
    }
}