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
            new IdentityResources.Profile(),
        };
    
    public static IEnumerable<ApiScope> ApiScopes => 
        new List<ApiScope>() 
        {
          new(name: "lojinha", "Lojinha"),
          new(name: "read", "Read data"),
          new(name: "write", "Write data"),
          new(name: "delete", "Delete data"),
        };
    
    public static IEnumerable<Client> Clients => 
        new List<Client>()
        {
            new() {
                ClientId = "product_web",
                ClientSecrets = { new Secret("gabriel_super_secret".Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile", "lojinha" }
            }
        };

}