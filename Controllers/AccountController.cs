using greenswamp.Database;
using greenswamp.Models;
using greenswamp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Npgsql;

namespace greenswamp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Auth> _signInManager;
        private readonly UserManager<Auth> _userManager;
        private readonly GreenswampContext _context;

        public AccountController(SignInManager<Auth> signInManager, UserManager<Auth> userManager, GreenswampContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new Login());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "The account is temporarily blocked. Try again later");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new Register());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existingUser = await _context.Users.AnyAsync(u => u.Username == model.Username);
            if (existingUser)
            {
                ModelState.AddModelError("Username", "This username is already occupied");
                return View(model);
            }

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            var auth = new Auth
            {
                UserName = model.Email,
                Email = model.Email
            };

            try
            {
                var result = await _userManager.CreateAsync(auth, model.Password);
                if (result.Succeeded)
                {
                    var user = new User
                    {
                        UserId = auth.Id,
                        Username = model.Username,
                        DisplayName = model.FullName,
                        CreatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
                        IsActive = true
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(auth, isPersistent: false);
                    return RedirectToAction("Index", "Feed", new { area = "Blog" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(model);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the account. Please try again.");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            return RedirectToLocal(returnUrl);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Feed", new { area = "Blog"});
        }
    }
}