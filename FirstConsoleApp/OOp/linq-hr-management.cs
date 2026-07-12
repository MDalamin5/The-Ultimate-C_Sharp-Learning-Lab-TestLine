using System;
using System.Collections.Generic;
using System.Linq;
using System.Net; // REQUIRED FOR THIS ASSIGNMENT!

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


            // task1: The boss wants to see a list of all employees who work in the "IT" department and are currently Active.
            var ActiveItDepEmployees = employees.Where(p => p.IsActive == true && p.Department == "IT");
            foreach(Employee person in ActiveItDepEmployees)
                Console.WriteLine($"ID: {person.Id} | Name: {person.Name} | Department: {person.Department}");


            // task2: A user searches for an employee with Id = 4. Find this employee and print their Name and Salary.

            var employeeId4 = employees.FirstOrDefault(p => p.Id == 4);
            if(employeeId4 != null)
                Console.WriteLine($"Name: {employeeId4.Name} | Salary: {employeeId4.Salary}");
            else
                Console.WriteLine("Data not Found.");

            // task3: The finance team wants to know: "Do we have ANY employee making more than 7500?"

            bool hasEmployee = employees.Any(p => p.Salary > 7500);

            if(hasEmployee)
                Console.WriteLine("Yes have");
            else
                Console.WriteLine("No Employee..");
                
                
            //task4: The security team needs a list of only the Names of all employees (they don't want to see Salaries or IDs). Create a List<string> containing just the names and print them.

            List<string> SecurityTeamData = employees.Select(p => p.Name).ToList();

            foreach(string name in SecurityTeamData)
                Console.Write($"Name: {name}");
            Console.WriteLine();
            

            // task5: Find all Active employees, order them by Salary, extract only their Names, and put them in a List<string>. (Bonus Hint: Use .OrderByDescending(e => e.Salary) if you want the highest paid at the top!)
                
            List<string> EmployeeList = employees.Where(p => p.IsActive == true)
            .OrderByDescending(p => p.Salary).Select(p => p.Name).ToList();

            foreach(string name in EmployeeList)
                Console.WriteLine($"Name: {name}");
                
            
            // task6: Imagine we are showing employees on a webpage, 2 per page. Write a query to get Page 2 (which means skip the first 2 employees, and take the next 2). Print their names.
            
            int pageSize = 2;
            int pageNumber = 2;

            var page2Employee = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            Console.WriteLine("Page 2 Employee Data...");
            
            foreach(var employee in page2Employee)
                Console.WriteLine($"Name: {employee.Name}");
                
              
                
            
        }
    }
}