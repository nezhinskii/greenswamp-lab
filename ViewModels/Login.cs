using System.ComponentModel.DataAnnotations;

namespace greenswamp.Models.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}