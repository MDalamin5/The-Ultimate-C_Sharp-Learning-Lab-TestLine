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
        [StringLength(20)]
        public required string Name {get; set;}
        [Required]
        public required string Description {get; set;}
    }
}