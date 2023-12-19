using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Lojinha.IdentityServer.Auth.Config;
using Lojinha.IdentityServer.Auth.Context;
using Microsoft.AspNetCore.Identity;

namespace Lojinha.IdentityServer.Auth.Services;

public class ProfileService : IProfileService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public ProfileService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {

        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());

        if (user is null) 
            return;

        ClaimsPrincipal claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user); 

        List<Claim> claims = claimsPrincipal.Claims.ToList();
        claims.AddRange(new List<Claim>() { 
            new(JwtClaimTypes.FamilyName, user.LastName), 
            new(JwtClaimTypes.GivenName, user.FirstName)
        });
        var roles = await _userManager.GetRolesAsync(user);

        roles.ToList().ForEach(role => claims.Add(new Claim(JwtClaimTypes.Role, role)));

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
        context.IsActive = user is not null;
    }
}