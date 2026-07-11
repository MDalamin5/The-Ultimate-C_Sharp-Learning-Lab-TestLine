namespace AdvString
{
    public class StringTestAssignment
    {
        public static void Run()
        {
            string input = "Helloss How are you!!";

            int vowelCount = input.Count(c => "aeiouAEIOU".Contains(c));
            Console.WriteLine($"Total vowel have: {vowelCount}");
            
            int consCount = input.Count(c => char.IsLetter(c) && !"aeiouAEIOU".Contains(c));
            Console.WriteLine(consCount);
            
            
                
        }
    }
}