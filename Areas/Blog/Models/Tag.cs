namespace greenswamp.Areas.Blog.Models
{
    public partial class Tag
    {
        public long TagId { get; set; }

        public string TagName { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public long? UsageCount { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
