using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationManagement.Models
{
    public class Designation
    {
        public Guid Id {get; set;}
        public required string Name {get; set;}
        public Guid DepartmentId {get; set;}
        public Department? Department {get; set;}
    }
}