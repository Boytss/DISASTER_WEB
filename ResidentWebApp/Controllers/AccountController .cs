using Microsoft.AspNetCore.Mvc;
using ResidentWebApp.Models;
using ResidentWebApp.Repositories;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using BCrypt.Net;
namespace ResidentWebApp.Controllers
{
     [Route("account")]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
         public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
          // Check if the email and password are provided
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
                return View();
            }

            // Get the user from the repository
            var user = await _userRepository.GetUserByUsernameAsync(email);

            // Check if the user exists and the password is correct
            if (user != null && user.Password == password)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, email) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return LocalRedirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return View();
            }
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View("Registration");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
                {
                    if (await _userRepository.UsernameExistsAsync(model.Username))
                    {
                        ModelState.AddModelError("Username", "This username is already taken");
                        return View("Registration", model);
                    }

                    try
                    {
                        await _userRepository.CreateUserAsync(model);
                        // Set the success message in TempData
                        TempData["SuccessMessage"] = "Registration successful. Please log in.";
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again.");
                        Console.Error.WriteLine($"Error in Register: {ex.Message}");
                    }
                }
                else
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.Error.WriteLine(error.ErrorMessage);
                    }
                }
                TempData["SuccessMessage"] = null; // Reset the success message
                return View("Registration", model);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
           try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Console.WriteLine("User signed out successfully");
                return Ok(new { message = "Logged out successfully" });
            }
         catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in Logout: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred during logout" });
            }
        }
    }
}