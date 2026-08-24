using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.DTOs
{
    public class CategoryUpdateDto
    {
        [Required(ErrorMessage = "Name is Required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must follow the Name Length.")]
        public  string Name {get; set;} = string.Empty;
        [Required]
        public  string Description {get; set;} = string.Empty;
    }
}