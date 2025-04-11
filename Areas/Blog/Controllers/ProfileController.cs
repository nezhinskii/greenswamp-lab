using greenswamp.Areas.Blog.Database;
using greenswamp.Areas.Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    public class ProfileController : Controller
    {
        private readonly GreenswampContext _context;

        public ProfileController(GreenswampContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Event)
                .Include(p => p.Tags)
                .Include(p => p.Interactions)
                .Where(p => p.UserId == user.UserId)
                .OrderByDescending(p => p.CreatedAt)
                .Take(10)
                .ToListAsync();

            var trendingPonds = await _context.TrendingPonds
                .OrderByDescending(tp => tp.RecentPosts)
                .Take(5)
                .ToListAsync();

            var upcomingEvents = await _context.Events
                .Where(e => e.EventTime > DateTime.Now)
                .OrderBy(e => e.EventTime)
                .Take(5)
                .ToListAsync();

            var viewModel = new ProfileViewModel
            {
                User = user,
                Posts = posts,
                TrendingPonds = trendingPonds,
                UpcomingEvents = upcomingEvents
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMorePosts(string username, int page = 1, int pageSize = 10)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Event)
                .Include(p => p.Tags)
                .Include(p => p.Interactions)
                .Where(p => p.UserId == user.UserId)
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PartialView("_PostList", posts);
        }
    }
}
