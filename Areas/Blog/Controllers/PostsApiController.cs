using greenswamp.Areas.Blog.Models;
using greenswamp.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using greenswamp.Models;
using greenswamp.Areas.Blog.ViewModels;

namespace greenswamp.Areas.Blog.Controllers
{
    [Area("Blog")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsApiController : ControllerBase
    {
        private readonly GreenswampContext _context;
        private readonly UserManager<Auth> _userManager;
        private readonly IWebHostEnvironment _environment;

        public PostsApiController(GreenswampContext context, UserManager<Auth> userManager, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
        {
            request.Content ??= "";
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { error = "User not found" });
            }

            if (request.IsEvent)
            {
                if (!request.EventTime.HasValue || string.IsNullOrWhiteSpace(request.Location) || string.IsNullOrWhiteSpace(request.Content))
                {
                    return BadRequest(new { error = "Event title, date and location are required" });
                }

                var post = new Post
                {
                    UserId = user.Id,
                    Content = request.Content?.Trim() ?? string.Empty,
                    PostType = PostType.Text,
                    CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                var eventItem = new Event
                {
                    EventTime = DateTime.SpecifyKind(request.EventTime.Value, DateTimeKind.Unspecified),
                    Location = request.Location,
                    HostOrg = request.HostOrg,
                    MaxCapacity = request.MaxCapacity,
                    Post = post
                };

                post.Event = eventItem;

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                var response = new
                {
                    post.PostId
                };

                return Ok(response);
            } else {
                if (string.IsNullOrWhiteSpace(request.Content) && request.Media == null)
                {
                    return BadRequest(new { error = "Content cannot be empty" });
                }

                Post? parentPost = null;
                if (request.ParentPostId.HasValue)
                {
                    parentPost = await _context.Posts.FindAsync(request.ParentPostId.Value);
                    if (parentPost == null)
                    {
                        return NotFound(new { error = "Parent post not found" });
                    }

                    var reribbInteraction = new Interaction
                    {
                        UserId = user.Id,
                        PostId = request.ParentPostId.Value,
                        InteractionType = InteractionType.Reribb,
                        CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                    };
                    _context.Interactions.Add(reribbInteraction);
                }

                var tagRegex = new Regex(@"\B#[\w\d_-]+");
                var tags = tagRegex.Matches(request.Content)
                    .Select(m => m.Value.Substring(1))
                    .Distinct()
                    .ToList();

                var cleanContent = tagRegex.Replace(request.Content, "").Trim();

                var post = new Post
                {
                    UserId = user.Id,
                    Content = cleanContent,
                    PostType = PostType.Text,
                    CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                    ParentPostId = request.ParentPostId
                };

                if (tags.Any())
                {
                    foreach (var tagName in tags)
                    {
                        var tag = await _context.Tags
                            .FirstOrDefaultAsync(t => t.TagName == tagName);
                        if (tag == null)
                        {
                            tag = new Tag
                            {
                                TagName = tagName,
                                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                                UsageCount = 1
                            };
                            _context.Tags.Add(tag);
                        }
                        else
                        {
                            tag.UsageCount = (tag.UsageCount ?? 0) + 1;
                        }
                        post.Tags.Add(tag);
                    }
                }

                if (request.Media != null)
                {
                    var allowedImageTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    var allowedVideoTypes = new[] { "video/mp4", "video/webm" };

                    if (allowedImageTypes.Contains(request.Media.ContentType))
                    {
                        post.PostType = PostType.Image;
                        post.MediaType = MediaType.Image;
                    }
                    else if (allowedVideoTypes.Contains(request.Media.ContentType))
                    {
                        post.PostType = PostType.Video;
                        post.MediaType = MediaType.Video;
                    }
                    else
                    {
                        return BadRequest(new { error = "Unsupported media type. Only images (jpeg, png, gif) or videos (mp4, webm) are allowed." });
                    }

                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Media.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await request.Media.CopyToAsync(stream);
                    }

                    post.MediaUrl = $"/uploads/{fileName}";
                }
                else
                {
                    post.PostType = PostType.Text;
                }

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                var response = new
                {
                    post.PostId
                };

                return Ok(response);
            }
        }

        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment([FromForm] long postId, [FromForm] string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { error = "User not found" });
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                return BadRequest(new { error = "Comment content cannot be empty" });
            }

            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return NotFound(new { error = "Post not found" });
            }

            var interaction = new Interaction
            {
                UserId = user.Id,
                PostId = postId,
                InteractionType = InteractionType.Comment,
                Content = content.Trim(),
                CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
            };

            _context.Interactions.Add(interaction);
            await _context.SaveChangesAsync();

            return Ok(new { interaction.InteractionId });
        }
    }
}