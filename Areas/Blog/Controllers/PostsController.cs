using greenswamp.Areas.Blog.Models;
using greenswamp.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("[area]/[controller]")]
    public class PostsController : Controller
    {
        private readonly GreenswampContext _context;

        public PostsController(GreenswampContext context)
        {
            _context = context;
        }

        [HttpGet("GetPostHtml/{postId}")]
        public async Task<IActionResult> GetPostHtml(long postId)
        {
            var post = await _context.Posts
                .Include(p => p.User)
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
    }
}
