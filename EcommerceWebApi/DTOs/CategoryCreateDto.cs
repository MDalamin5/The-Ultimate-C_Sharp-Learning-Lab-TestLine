using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.DTOs
{
    public class CategoryCreateDto
    {   
        [Required(ErrorMessage ="Category Name is Required.")]
        public string? Name {get; set;}
        public string Description {get; set;} = string.Empty;
    }
}