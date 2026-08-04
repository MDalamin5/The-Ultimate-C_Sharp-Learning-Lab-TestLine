using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCorePractice.Dots
{
    public class CreateBookDto
    {
        public string? Title {get; set;}
        public string? Description {get; set;}
        public string? Author {get; set;}
        public decimal Price {get; set;}
    }
}