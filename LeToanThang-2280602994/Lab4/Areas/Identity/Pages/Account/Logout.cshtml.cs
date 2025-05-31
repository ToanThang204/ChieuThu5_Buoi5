using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Lab4.Models; // Thêm namespace để dùng ApplicationUser

namespace Lab4.Areas.Identity.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                if (_signInManager.IsSignedIn(User))
                {
                    _logger.LogInformation("User {UserName} is logging out.", User.Identity.Name);

                    // Xóa toàn bộ session
                    HttpContext.Session.Clear();

                    // Xóa cookie xác thực
                    await _signInManager.SignOutAsync();

                    // Xóa tất cả cookie trong trình duyệt
                    foreach (var cookie in Request.Cookies.Keys)
                    {
                        Response.Cookies.Delete(cookie);
                    }

                    _logger.LogInformation("User logged out successfully.");
                }
                else
                {
                    _logger.LogWarning("No user is currently signed in to log out.");
                }

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during logout.");
                return RedirectToPage("/Error");
            }
        }
    }
}