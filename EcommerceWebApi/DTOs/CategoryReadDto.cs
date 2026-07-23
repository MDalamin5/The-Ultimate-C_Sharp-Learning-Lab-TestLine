
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DTOs
{
    public class CategoryReadDto
    {
        public string? Name {get; set;}
        public string? Description {get; set;} = string.Empty;
        public DateTime CreatedAt {get; set;}
    }
}