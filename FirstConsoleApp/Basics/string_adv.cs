namespace AdvString
{
    public class StringTest
    {
        public static void Run()
        {
            //Check the string have the whitespace, null, empty
            string input = "  hello";
            if(string.IsNullOrWhiteSpace(input))
                Console.WriteLine("Invalid string..");
            else
                Console.WriteLine("Good to go...");


            // split, and join
            string data = "Mango, banana, Orange";
            string[] fruits = data.Split(',');
            fruits[1] = "Yellow-Banana";

            foreach(string fruit in fruits)
                Console.Write($"{fruit} ");

            Console.WriteLine("\nJoin back together");

            string newData = string.Join(" | ", fruits);

            Console.WriteLine($"The new Fruits: {newData}");

            string email = "mdalamin@gmail.com";
            int atIndex = email.IndexOf("@");

            Console.WriteLine($"The indexNumber: {atIndex}");
            
            // Get everything after the @
            string domain = email.Substring(atIndex + 1); 
            Console.WriteLine(domain); 


            // reverse sting
            string secret = "Csharp";

            string reverse = new string(secret.Reverse().ToArray());
            Console.WriteLine(reverse);
            
            
                
        }
    }
}