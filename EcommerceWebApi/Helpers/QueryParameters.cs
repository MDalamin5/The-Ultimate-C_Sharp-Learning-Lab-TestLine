using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Helpers
{
    public class QueryParameters
    {   
        private const int MaxPageSize = 50;
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 6;
        public string? Search {get; set;}

        public QueryParameters Validate()
        {
            if(PageNumber < 1)
                PageNumber = 1;
        
            if(PageSize < 1)
                PageSize = 6;
            
            if(PageSize > MaxPageSize)
                PageSize = MaxPageSize;

            return this;
        }
    }

    
}