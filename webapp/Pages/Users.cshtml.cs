using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace webapp.Pages;
public class UsersModel : PageModel
{
    private readonly ILogger<UsersModel> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public List<IdentityUser> users { get; set; }
    
    public UsersModel(ILogger<UsersModel> logger, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public void OnGet()
    {
        users = _userManager.Users.ToList();
    }
}

