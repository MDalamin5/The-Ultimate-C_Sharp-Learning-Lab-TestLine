using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Models
{
    public class Product
    {
        public Guid ProductId {get; set;}
        public string Name {get; set; } = string.Empty;
        public decimal Price {get; set;}
        public Guid CategoryId {get; set;}
        public Category? Category {get; set;}
    }
}