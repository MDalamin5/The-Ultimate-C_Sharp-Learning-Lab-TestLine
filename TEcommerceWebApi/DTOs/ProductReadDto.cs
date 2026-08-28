using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.DTOs
{
    public class ProductReadDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        
        // We can flatten the data: include the Category details right inside the Product response!
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}