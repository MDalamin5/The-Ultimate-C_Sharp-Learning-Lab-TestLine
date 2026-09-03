using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEcommerceWebApi.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //navigate to order table
        
    }
}