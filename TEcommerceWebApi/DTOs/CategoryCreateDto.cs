using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 20 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}