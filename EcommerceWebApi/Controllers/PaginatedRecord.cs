using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    public class PaginatedRecord<T>
    {
        public IEnumerable<T> Items {get; set;} = new List<T>();

        public int TotalCount {get; set;}
        public int PageNumber {get; set;}
        public int PageSize {get; set;}
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}