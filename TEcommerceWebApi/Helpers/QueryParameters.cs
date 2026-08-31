using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Helpers
{
    public class QueryParameters
    {
        private const int MAX_PAGE_SIZE = 6;
        public int PageNumber {get; set;} = 1;
        public int PageSize {get; set;} = 4;
        public string? SearchValue {get; set;} = null;
        public string ?SortOrder {get; set;} = null;

        public QueryParameters Validate()
        {
            if(PageSize > MAX_PAGE_SIZE)
            {
                PageSize = MAX_PAGE_SIZE;
            }
            if(PageSize < 1)
            {
                PageSize = 1;
            }

            return this;
        }
    }
}