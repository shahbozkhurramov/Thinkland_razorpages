using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using webapp.Models;

namespace webapp.Pages.Account;

public class LoginModel: PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    [BindProperty]
    public LoginViewModel LoginViewModel { get; set; }
    
    public LoginModel(ILogger<LoginModel> logger,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signinManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signinManager;
    }

    public void OnGet()
    {
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(!ModelState.IsValid)
        {
            return Page();
        }

        var user = _userManager.Users.FirstOrDefault(u => u.UserName == LoginViewModel.UserName);
        if(user == default)
        {
            _logger.LogError("Error occured while logging in");
            return Page();
        }

        var result = await _signInManager.PasswordSignInAsync(user, LoginViewModel.Password, false, false);
        if(result.Succeeded)
        {
            _logger.LogInformation($"{User.Identity.Name} successfully logged in.");
            return LocalRedirect("/");
        }
        else
        {
            _logger.LogError("Error occured");
            return Page();
        }
    }
}