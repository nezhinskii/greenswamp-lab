using greenswamp.Areas.Blog.Models;

namespace greenswamp.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Username { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string? AvatarUrl { get; set; }

        public string? Bio { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public virtual Auth? Auth { get; set; } = null!;

        public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}