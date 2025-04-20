using Microsoft.AspNetCore.Identity;

namespace greenswamp.Models
{
    public class Auth : IdentityUser<long>
    {
        public DateTime? LastLogin { get; set; }
        public string? ResetToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
        public virtual User User { get; set; } = null!;

    }
}