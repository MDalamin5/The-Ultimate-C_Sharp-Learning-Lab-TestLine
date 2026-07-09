namespace FirstConsoleApp.ReadOnly
{
    public class Student
    {
        public readonly int age; // read only variable value set by constructor;

        public Student(int age)
        {
            this.age = age;
        }
    }

    public class Driver
    {
        public static void Hello()
        {
            Student s1 = new Student(44);
            Console.WriteLine($"The age is: {s1.age}");
        }
    }
}