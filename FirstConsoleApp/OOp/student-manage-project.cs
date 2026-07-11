using System.Globalization;

namespace ManagementProject
{
    public class Student
    {
        public string name {get; set;} = "";
        private DateTime _dateOfBirth;
        public string rollNumber {get; set;} = "";


        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("Date can not be future pagla...");
                }
                _dateOfBirth = value;
            }

        }
        public Student(string name, DateTime dateOfBirth, string rollNumber)
        {
            this.name = name;
            DateOfBirth = dateOfBirth;
            this.rollNumber = rollNumber;
        }

        public void Display()
        {
            Console.WriteLine($"Name: {name}, DateOfBirth: {DateOfBirth}, Roll: {rollNumber}");
            
        }
    }


    public class StudentDriver
    {
        public static void Run()
        {
            try
            {
                DateTime dob1 = new DateTime(2000, 5, 15);
                DateTime dob2 = new DateTime(1999, 11, 25);
                Student s1 = new Student("Md Al Amin", dob1, "1811903");
                Student s2 = new Student("Miad", dob2, "1811904");

                s1.Display();
                s2.Display();
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine($"The error: {ex.Message}");
                
            }
        }
    }
}