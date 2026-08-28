using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.DTOs
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name {get; set; } = string.Empty;
        [Range(0.01, 10000.0)]
        public decimal Price {get; set;}

        [Required]
        public Guid CategoryId {get; set;}
    }
}