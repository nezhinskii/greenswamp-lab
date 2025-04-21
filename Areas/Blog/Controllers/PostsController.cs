using greenswamp.Areas.Blog.Models;
using greenswamp.Database;
using greenswamp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("[area]/[controller]")]
    public class PostsController : Controller
    {
        private readonly GreenswampContext _context;
        private readonly UserManager<Auth> _userManager;

        public PostsController(GreenswampContext context, UserManager<Auth> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("GetPostHtml/{postId}")]
        public async Task<IActionResult> GetPostHtml(long postId)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.ParentPost)
                .Include(p => p.Tags)
                .Include(p => p.Event)
                .Include(p => p.Interactions)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return NotFound();
            }

            return PartialView("_PostList", new List<Post> { post });
        }

        private async Task<User?> GetCurrentUserAsync()
        {
            var auth = await _userManager.GetUserAsync(User);
            if (auth == null)
                return null;

            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == auth.Id);
        }

        [HttpGet("GetFullPostHtml/{postId}")]
        public async Task<IActionResult> GetFullPostHtml(long postId)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.ParentPost)
                .Include(p => p.Tags)
                .Include(p => p.Event)
                .Include(p => p.Interactions)
                    .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(p => p.PostId == postId);

            if (post == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserAsync();

            ViewData["IsFullView"] = true;
            ViewData["CurrentUser"] = currentUser;

            return PartialView("_PostList", new List<Post> { post });
        }
    }
}
