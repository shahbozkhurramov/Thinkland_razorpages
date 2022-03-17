using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using webapp.Models;

namespace webapp.Pages.Account;

public class RegisterModel: PageModel
{
    private readonly ILogger<RegisterModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    [BindProperty]
    public RegisterViewModel RegisterViewModel { get; set; }
    
    public RegisterModel(ILogger<RegisterModel> logger,
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
        if(await _userManager.Users.AnyAsync(u => u.Email == RegisterViewModel.Email))
        {
            _logger.LogError("Email already exists");
            return Page();
        }
        if(await _userManager.Users.AnyAsync(u => u.UserName == RegisterViewModel.UserName))
        {
            _logger.LogError("Username already exists");
            return Page();
        }
        var user = new IdentityUser()
        {
            UserName = RegisterViewModel.UserName,
            Email = RegisterViewModel.Email,
        };

        await _userManager.CreateAsync(user, RegisterViewModel.Password);
        return RedirectToPage("/Index");
    }
}