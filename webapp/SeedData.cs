using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace webapp;

public class SeedData : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SeedData> _logger;
    public UserManager<IdentityUser> _userManager { get; private set; }
    public RoleManager<IdentityRole> _roleManager { get; private set; }

    public SeedData(IServiceProvider serviceProvider, ILogger<SeedData> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        if(await _roleManager.FindByNameAsync("admin") == null)
        {
            var roleResult = await _roleManager.CreateAsync(new IdentityRole("admin"));
            if(roleResult.Succeeded)
            {
                _logger.LogInformation("admin role created successfully.");
            }
            else
            {
                _logger.LogError("Creating admin role failed...", roleResult.Errors);
            }
        }

        if(await _userManager.FindByNameAsync("admin") == null)
        {
            var newUser = new IdentityUser()
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            };
            var identityResult = await _userManager.CreateAsync(newUser, "admin");
            if(identityResult.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(newUser, "admin");
                if(identityResult.Succeeded)
                {
                    _logger.LogInformation("User with admin role created successfully...");
                }
                else
                {
                    _logger.LogError("Error in creating user with admin role...", roleResult.Errors);
                }
            }
            else
            {
                _logger.LogError("Assigning admin role to user failed...", identityResult.Errors);
            }
        }
    }
}