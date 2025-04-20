using greenswamp.Areas.Blog.Models;
using greenswamp.Models;

namespace greenswamp.Areas.Blog.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<TrendingPond> TrendingPonds { get; set; } = new List<TrendingPond>();
        public List<Event> UpcomingEvents { get; set; } = new List<Event>();
        public User? CurrentUser { get; set; }
    }
}
