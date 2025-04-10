using greenswamp.Models;
using greenswamp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace greenswamp.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IContactFormService _contactFormService;
        public ContactModel(IContactFormService contactFormService)
        {
            _contactFormService = contactFormService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [RegularExpression(@"^\S+@\S+\.edu$", ErrorMessage = "Email must be from a .edu domain.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please select a message topic.")]
        public string Subject { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var formData = new ContactFormData
            {
                Name = Name,
                Email = Email,
                Subject = Subject,
                Message = Message,
                SubmissionDate = DateTime.UtcNow
            };

            await _contactFormService.SaveToCsvAsync(formData);
            return Page();
        }
    }
}