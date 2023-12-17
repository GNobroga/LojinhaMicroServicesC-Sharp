using System.Security.Claims;
using IdentityModel;
using Lojinha.IdentityServer.Auth.Config;
using Lojinha.IdentityServer.Auth.Context;
using Microsoft.AspNetCore.Identity;

namespace Lojinha.IdentityServer.Auth.Services;

public class AppService 
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AppService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateRoles()
    {
        List<string> roles = new() 
        {
            ApplicationConfig.ADMIN,
            ApplicationConfig.CLIENT
        };

        foreach (var role in roles)
        {
            if (await _roleManager.RoleExistsAsync(role)) continue;
            await _roleManager.CreateAsync(new() { Name = role });
        }
    }

    public async Task CreateUsers()
    {
        if (await _userManager.FindByEmailAsync("admin@admin.com") is null)
        {
            var user = new ApplicationUser {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FirstName = "Gabriel",
                LastName = "Cardoso Girarde",
                PhoneNumber = "+55 (28) 99950-5410"
            };      

            var result = await _userManager.CreateAsync(user, "Gabrielcar34@");   

            if (!result.Succeeded) return;

            await _userManager.AddToRoleAsync(user, ApplicationConfig.ADMIN);
            await _userManager.AddClaimsAsync(user, new Claim[] {
                new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(JwtClaimTypes.GivenName, user.FirstName),
                new(JwtClaimTypes.FamilyName, user.LastName),
                new(JwtClaimTypes.Role, ApplicationConfig.ADMIN),
            });

        }

        if (await _userManager.FindByEmailAsync("client@client.com") is null)
        {
            var user = new ApplicationUser {
                UserName = "client",
                Email = "client@client.com",
                EmailConfirmed = true,
                FirstName = "Gabriel",
                LastName = "Cardoso Girarde",
                PhoneNumber = "+55 (28) 99950-5410"
            };      

            var result = await _userManager.CreateAsync(user, "Gabrielcar34@");   

            if (!result.Succeeded) return;

            await _userManager.AddToRoleAsync(user, ApplicationConfig.CLIENT);
            await _userManager.AddClaimsAsync(user, new Claim[] {
                new(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new(JwtClaimTypes.GivenName, user.FirstName),
                new(JwtClaimTypes.FamilyName, user.LastName),
                new(JwtClaimTypes.Role, ApplicationConfig.CLIENT),
            });

        }
    }
}