using System.Threading.Channels;

namespace FirstConsoleApp.ReadOnly
{
    public class Student
    {
       private int _fieldName;
       public int PropertyName
       {
           get => _fieldName;
           set
           {
               if (value > 0)
               {
                   _fieldName = value;
               }
           }
       }
       public Student(int age)
       {
           this.PropertyName = age;
       }
    }

    public class Driver
    {
        public static void Hello()
        {
            Student s1 = new Student(44);
            Console.WriteLine($"The age is: {s1.PropertyName}");
            
            
            
        }
    }
}