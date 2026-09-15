using System.ComponentModel.DataAnnotations;

namespace TEcommerceWebApi.DTOs
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;
    }
}