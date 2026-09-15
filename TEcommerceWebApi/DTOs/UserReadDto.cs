using System;

namespace TEcommerceWebApi.DTOs
{
    public class UserReadDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalOrdersCount { get; set; }
    }
}