namespace FirstConsoleApp.Alamin
{
    class Encapculationss
    {
        // 1. Use Auto-Properties for simple data (No logic needed here)
        public string Name { get; set; } = "";
        public string University { get; set; } = "";

        // 2. Use a Backing Field for data with logic (The Barrier)
        private int _age;
        public int Age
        {
            get => _age;
            set 
            {
                if (value > 17) _age = value; 
            }
        }

        // 3. Constructor uses the Properties (Capital letters)
        // This ensures that even during 'new', the Age barrier is checked.
        public Encapculationss(string name, string university, int age)
        {
            Name = name;
            University = university;
            Age = age; // Triggers the 'if (value > 17)' logic
        }

        public void Display()
        {
            // Use Properties here too for consistency
            Console.WriteLine($"Name: {Name}, Age: {Age}, Uni: {University}");
        }
    }


    class DemoEnc
    {
        public static void EncRun()
        {
            Encapculationss s1 = new Encapculationss("GTR...", "NSU", 15);
            s1.Display();
        }
    }
}