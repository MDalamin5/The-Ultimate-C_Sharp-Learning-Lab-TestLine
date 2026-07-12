using System;
using System.Collections.Generic;
using System.Linq; // REQUIRED FOR THIS ASSIGNMENT!

namespace FirstConsoleApp.LiNQ
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Department { get; set; } = "";
        public double Salary { get; set; }
        public bool IsActive { get; set; }
    }

    public class LinqAssignment
    {
        public static void Run()
        {
            // Our Fake Database
            List<Employee> employees = [
                new Employee { Id = 1, Name = "Alamin", Department = "IT", Salary = 5000, IsActive = true },
                new Employee { Id = 2, Name = "Miad", Department = "HR", Salary = 4000, IsActive = true },
                new Employee { Id = 3, Name = "Hasan", Department = "IT", Salary = 7000, IsActive = false },
                new Employee { Id = 4, Name = "Kamrul", Department = "Sales", Salary = 3000, IsActive = true },
                new Employee { Id = 5, Name = "Sakib", Department = "IT", Salary = 8000, IsActive = true },
                new Employee { Id = 6, Name = "Tamim", Department = "HR", Salary = 4500, IsActive = false }
            ];

            // ==========================================
            // WRITE YOUR LINQ QUERIES BELOW THIS LINE
            // ==========================================
            
        }
    }
}