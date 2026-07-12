using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.VisualBasic;

namespace FirstConsoleApp.LiNQ
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public bool IsInStock { get; set; }
    }

// Pretend we just loaded this from a Database

    public class LinqDriver
    {
        public static void Run()
        {
            List<Product> database = [
                new Product { Id = 1, Name = "Laptop", Price = 1200.00, IsInStock = true },
                new Product { Id = 2, Name = "Mouse", Price = 25.50, IsInStock = true },
                new Product { Id = 3, Name = "Keyboard", Price = 75.00, IsInStock = false },
                new Product { Id = 4, Name = "Monitor", Price = 300.00, IsInStock = true }
            ];
            // Finding the singeIdem

            var selectProduct = database.FirstOrDefault(p => p.Id == 1);
            if(selectProduct != null)
                Console.WriteLine($"Found: {selectProduct.Name}");
            else
                Console.WriteLine("404 not found error...");

            // check for the existence
            bool hasCheapItems = database.Any(p => p.Price < 50);
            if(hasCheapItems)
                Console.WriteLine("Check out our bargain deals...");

            // Data Transformations select()

            List<string> ProductNames = database.Select(p => p.Name).ToList();

            foreach(string product in ProductNames)
                Console.Write($"{product}, ");
            Console.WriteLine();


            // Pagination
            int pageSize = 2;
            int pageNumber = 2;

            var pageTwoItems = database
            .Skip((pageNumber -1) * pageSize)
            .Take(pageSize)
            .ToList();

            foreach(var product in pageTwoItems)
                Console.Write($"{product.Name}, ");


            // Deletes everything that matches the rule from the original list.
            int deletedCount = database.RemoveAll(p => p.IsInStock == false);

            Console.WriteLine($"Cleanup finished: Deleted {deletedCount} out-of-stock items.");

            // task: Give me the Names of all In-Stock products, ordered by Price (cheapest first).

            var result = database
            .Where(p => p.IsInStock == true)
            .OrderBy(p => p.Price)
            .Select(p => p.Name)
            .ToList();
                
            
            foreach(var product in result)
                Console.Write($"{product}, ");
                
                
                
        }

        

        
    }

}