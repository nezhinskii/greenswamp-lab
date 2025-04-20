namespace greenswamp.Areas.Blog.ViewModels
{
    public class CreatePostRequest
    {
        public bool IsEvent { get; set; }
        public string? Content { get; set; }
        public IFormFile? Media { get; set; }
        public DateTime? EventTime { get; set; }
        public string? Location { get; set; }
        public string? HostOrg { get; set; }
        public long? MaxCapacity { get; set; }
    }
}
