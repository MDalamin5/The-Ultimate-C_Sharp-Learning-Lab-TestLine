using System;
using System.ComponentModel.DataAnnotations;

namespace TEcommerceWebApi.DTOs
{
    public class ProductUpdateDto
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 100000.00)]
        public decimal Price { get; set; }

        [Range(0, 100000)]
        public int StockQuantity { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
    }
}