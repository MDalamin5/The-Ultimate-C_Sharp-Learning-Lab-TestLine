using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationManagement.Models
{
    public class Department
    {
        public Guid Id {get; set;}
        public required string Name {get; set;}
        public ICollection<Designation> Designations = new List<Designation>();
    }
}