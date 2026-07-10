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
    }
}