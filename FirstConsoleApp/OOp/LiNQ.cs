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
                
                
        }

        

        
    }

}