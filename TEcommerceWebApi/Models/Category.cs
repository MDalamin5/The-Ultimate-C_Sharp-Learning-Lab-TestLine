using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Models
{
    public class Category
    {
        public Guid CategoryId {get; set;}
        public string Name {get; set;} = string.Empty;
        public string ?Description {get; set;}
        public DateTime CreatedAt {get; set;}
        public ICollection<Product> Products {get; set;} = new List<Product>();
    }
}