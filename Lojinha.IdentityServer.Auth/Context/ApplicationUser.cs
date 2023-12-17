using Microsoft.AspNetCore.Identity;

namespace Lojinha.IdentityServer.Auth.Context;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}