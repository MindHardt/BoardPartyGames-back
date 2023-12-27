using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters.")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Username cannot contain spaces.")]
        public string Username { get; set; }

        // [Required(ErrorMessage = "Email is required.")]
        // [EmailAddress(ErrorMessage = "Invalid email address.")]
        // public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; }
    }
}
