namespace TEcommerceWebApi.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        // 📦 Added Inventory/Stock column:
        public int StockQuantity { get; set; } = 0;

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsDeleted { get; set; } = false; // 👈 Add this

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}