using Duende.IdentityServer.Models;

namespace Lojinha.IdentityServer.Auth.Config;

public static class ApplicationConfig 
{
    public const string ADMIN = "Admin";
    public const string CLIENT = "Client";

    public static IEnumerable<IdentityResource> IdentityResources => 
        new List<IdentityResource>() 
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
    
    public static IEnumerable<ApiScope> ApiScopes => 
        new List<ApiScope>() 
        {
          new(name: "web", "Lojinha WEB")
        };
    
    public static IEnumerable<Client> Clients => 
        new List<Client>()
        {
            new() {
                ClientId = "lojinha_web",
                RedirectUris = {
                    "http://localhost:5124/home"
                },
                ClientSecrets = { new Secret("lojinha_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = { "web", "profile", "openid", "email" }
            }
        };

}