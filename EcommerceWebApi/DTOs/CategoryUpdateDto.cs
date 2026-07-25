using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWebApi.DTOs
{
    public class CategoryUpdateDto
    {
       
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category Name At-list 3 Char.")]
        public string? Name {get; set;}
        [StringLength(300, MinimumLength = 30, ErrorMessage = "Descriptions is More than 30 char.")]
        public string Description {get; set;} = string.Empty;
    }
}