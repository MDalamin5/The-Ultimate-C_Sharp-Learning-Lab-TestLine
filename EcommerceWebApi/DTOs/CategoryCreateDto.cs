using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DTOs
{
    public class CategoryCreateDto
    {
        public string? Name {get; set;}
        public string Description {get; set;} = string.Empty;
    }
}