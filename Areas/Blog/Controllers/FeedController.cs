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
    public class FeedController : Controller
    {
        private readonly GreenswampContext _context;
        private readonly UserManager<Auth> _userManager;

        public FeedController(GreenswampContext context, UserManager<Auth> userManager)
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

        public async Task<IActionResult> Index()
        {
            return await BuildPosts();
        }

        [Route("Blog/Ponds/{tag}")]
        public async Task<IActionResult> Ponds(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return NotFound();
            }
            if (!await _context.Tags.AnyAsync(t => t.TagName == tag))
            {
                return NotFound();
            }
            return await BuildPosts(tag);
        }

        public async Task<IActionResult> BuildPosts(string? tag = null)
        {
            IQueryable<Post> postsQuery = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Event)
                .Include(p => p.Tags)
                .Include(p => p.Interactions);

            if (!string.IsNullOrEmpty(tag))
            {
                postsQuery = postsQuery.Where(p => p.Tags.Any(t => t.TagName == tag));
            }

            var posts = await postsQuery
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

            var currentUser = await GetCurrentUserAsync();

            var viewModel = new FeedViewModel
            {
                CurrentUser = currentUser,
                Posts = posts,
                TrendingPonds = trendingPonds,
                UpcomingEvents = upcomingEvents,
                Tag = tag
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMorePosts(int page = 1, int pageSize = 10, string? tag = null)
        {
            IQueryable<Post> postsQuery = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Event)
                .Include(p => p.Tags)
                .Include(p => p.Interactions);

            if (!string.IsNullOrEmpty(tag))
            {
                postsQuery = postsQuery.Where(p => p.Tags.Any(t => t.TagName == tag));
            }

            var posts = await postsQuery
                .OrderByDescending(p => p.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return PartialView("_PostList", posts);
        }
    }
}
