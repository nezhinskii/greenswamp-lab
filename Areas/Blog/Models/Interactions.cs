using greenswamp.Models;

namespace greenswamp.Areas.Blog.Models
{
    public enum InteractionType
    {
        Comment,
        Reribb,
        Like,
        Rsvp
    }

    public partial class Interaction
    {
        public long InteractionId { get; set; }

        public long UserId { get; set; }

        public long PostId { get; set; }

        public InteractionType InteractionType { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Content { get; set; }

        public virtual Post Post { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
