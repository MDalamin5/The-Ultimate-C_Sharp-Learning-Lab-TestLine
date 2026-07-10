using System.Globalization;

namespace ManagementProject
{
    public class Student
    {
        public string name {get; set;} = "";
        public string dateOfBirth {get; set;} = "";
        public string rollNumber {get; set;} = "";

        public Student(string name, string dateOfBirth, string rollNumber)
        {
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.rollNumber = rollNumber;
        }
    }
}