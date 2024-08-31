using Coupons.Database;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SchoolCoupons.Models
{
    public class BookCouponsModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<BookCouponsModel> _logger;
        private readonly UserManager<User> _userManager;
        private readonly CouponsContext _dbContext;

        public BookCouponsModel(SignInManager<User> signInManager, 
            ILogger<BookCouponsModel> logger, 
            UserManager<User> userManager, 
            CouponsContext dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [BindProperty]
        [Required]
        public InputModel? Input { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
                
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameOrEmailAsync(Input.UserName, Input.Password);

                var result = await _signInManager.PasswordSignInAsync(user.Result, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                var roleId = _dbContext.UserRoles.Where(userRole => userRole.UserId == user.Result.Id).Select(userRole => userRole.RoleId).Single();
                var role = _dbContext.Roles.Find(roleId).Name;

                //returnUrl = $"/{role}";

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    //return LocalRedirect(returnUrl);
                    return RedirectToAction("Index", $"{role}");
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }

}

