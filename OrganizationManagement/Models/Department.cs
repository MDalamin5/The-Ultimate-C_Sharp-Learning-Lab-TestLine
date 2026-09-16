using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationManagement.Models
{
    public class Department
    {
        [Key]
        public Guid Id {get; set;}
        public required string Name {get; set;}
        
    }
}