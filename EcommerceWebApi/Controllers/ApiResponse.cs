using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceWebApi.Controllers
{
    public class ApiResponse<T>
    {
        public bool Success {get; set;}
        public string? Message {get; set;}
        public T? Data {get; set;}
        public List<string>? Errors {get; set;}

        public int StatusCode {get; set;}
        public DateTime TimeStamp {get; set;}

        // Constructor for Successful Response

        private ApiResponse(bool success, string message, T data, int statusCode, List<string>errors)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
            StatusCode = statusCode;
            TimeStamp = DateTime.UtcNow;
        }

        // static method for creating a successful response.
        public static ApiResponse<T> SuccessResponse(T data, string message, int statusCode)
        {
            return new ApiResponse<T>(true, message, data, statusCode, null);
        }

        public static ApiResponse<T> ErrorResponse(List<string> errors, int statusCode, string message = "")
        {
            return new ApiResponse<T>(success: true, message: message, data: default(T), errors: errors, statusCode: statusCode);
        }
    }

    
    
}