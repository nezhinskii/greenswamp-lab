using greenswamp.Services;
using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public SubscribeController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody] SubscribeRequest request)
        {
            if (string.IsNullOrEmpty(request?.Email) || !IsValidEmail(request.Email))
            {
                return BadRequest("Invalid email address.");
            }

            try
            {
                _emailService.SendEmail(
                    toEmail: request.Email,
                    subject: "Welcome to Greenswamp!",
                    body: $"Thanks for subscribing, {request.Email}!",
                    isBodyHtml: true
                );

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return StatusCode(500, "Error sending email.");
            }
        }

        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^\S+@\S+\.\S+$");
        }
    }

    public class SubscribeRequest
    {
        public string Email { get; set; }
    }
}