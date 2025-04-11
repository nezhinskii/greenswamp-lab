using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace greenswamp.Areas.Blog.Models
{
    public enum PostType
    {
        Text,
        Image,
        Video
    }

    public enum MediaType
    {
        Image,
        Video
    }

    public class Post
    {
        public long PostId { get; set; }

        public long UserId { get; set; }

        public string Content { get; set; } = null!;

        public PostType PostType { get; set; }

        public string? MediaUrl { get; set; }

        public MediaType? MediaType { get; set; } = null;

        public string? AltText { get; set; }

        public string? ThumbnailUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public long? ParentPostId { get; set; }

        public virtual Event? Event { get; set; }

        public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

        public virtual ICollection<Post> InverseParentPost { get; set; } = new List<Post>();

        public virtual Post? ParentPost { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}