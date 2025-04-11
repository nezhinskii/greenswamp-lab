using greenswamp.Areas.Blog.Database;
using greenswamp.Areas.Blog.Models;
using greenswamp.Areas.Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostController : Controller
    {
        private readonly GreenswampContext _context;

        public PostController(GreenswampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int postId)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Event)
                .Include(p => p.Tags)
                .Include(p => p.Interactions)
                    .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return NotFound();
            }

            var trendingPonds = await _context.TrendingPonds
                .OrderByDescending(tp => tp.RecentPosts)
                .Take(5)
                .ToListAsync();

            var upcomingEvents = await _context.Events
                .Where(e => e.EventTime > DateTime.Now)
                .OrderBy(e => e.EventTime)
                .Take(5)
                .ToListAsync();

            var viewModel = new FeedViewModel
            {
                Posts = new List<Post> { post },
                TrendingPonds = trendingPonds,
                UpcomingEvents = upcomingEvents
            };

            return View(viewModel);
        }
    }
}
