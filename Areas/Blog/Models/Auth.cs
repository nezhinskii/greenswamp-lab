namespace greenswamp.Areas.Blog.Models
{
    public partial class Auth
    {
        public long UserId { get; set; }

        public string PasswordHash { get; set; } = null!;

        public DateTime? LastLogin { get; set; }

        public string? ResetToken { get; set; }

        public DateTime? TokenExpiry { get; set; }

        public virtual User User { get; set; } = null!;
    }

}
