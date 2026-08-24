using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success {get; set;}
        public string Message {get; set;} = string.Empty;
        public T? Data {get; set;}
        public List<string>? Errors {get; set;}
        public int StatusCode {get; set;}
        public DateTime TimeStamp {get; set;}


        private ApiResponse(bool success, string message, T data, List<string> errors, int statusCode)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }

        //Static method for the creating a successful response.
        public static ApiResponse<T> SuccessResponse (T data, int statusCode, string message)
        {
            return new ApiResponse<T>(true, message, data, null, statusCode);
        }
    }
}