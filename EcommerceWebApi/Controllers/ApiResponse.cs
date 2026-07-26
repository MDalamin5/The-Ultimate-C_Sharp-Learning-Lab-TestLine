using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success {get; set;}
        public string? Messages {get; set;}
        public T? Data {get; set;}
        public List<string>? Errors {get; set;}

        public int StatusCode {get; set;}
        public DateTime TimeStamp {get; set;}
    }
}