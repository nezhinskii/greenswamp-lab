using greenswamp.Areas.Blog.Models;
using greenswamp.Areas.Blog.ViewModels;
using greenswamp.Database;
using greenswamp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class PostController : Controller
    {
        private readonly GreenswampContext _context;
        private readonly UserManager<Auth> _userManager;

        public PostController(GreenswampContext context, UserManager<Auth> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var auth = await _userManager.GetUserAsync(User);
            if (auth == null)
                return null;

            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == auth.Id);
        }

        public async Task<IActionResult> Index(int postId)
        {
            var currentUser = await GetCurrentUserAsync();

            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.ParentPost)
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
                CurrentUser = currentUser,
                Posts = new List<Post> { post },
                TrendingPonds = trendingPonds,
                UpcomingEvents = upcomingEvents
            };

            return View(viewModel);
        }
    }
}
