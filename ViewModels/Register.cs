using System.ComponentModel.DataAnnotations;

namespace greenswamp.Models.ViewModels
{
    public class Register
    {
        [Required(ErrorMessage = "Full name required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Username required")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "The username must be between 3 and 20 characters long and contain only letters, numbers, or underscores")]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Email required")]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The password must contain at least 6 characters.", MinimumLength = 6)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; } = null!;
    }
}