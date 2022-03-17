
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapp.Models;

namespace webapp.Pages.Account;

public class LogoutModel: PageModel
{
    private readonly ILogger<LogoutModel> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LogoutModel(ILogger<LogoutModel> logger,
        SignInManager<IdentityUser> signinManager)
    {
        _logger = logger;
        _signInManager = signinManager;
    }

    public async Task<IActionResult> OnGet()
    {
        if(_signInManager.IsSignedIn(User))
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"{User.Identity.Name} successfully logged out...");
        }
        return LocalRedirect("/Index");
    }
}