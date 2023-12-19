using Duende.IdentityServer.Services;
using Lojinha.IdentityServer.Auth.Config;
using Lojinha.IdentityServer.Auth.Context;
using Lojinha.IdentityServer.Auth.Services;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();


builder.Services.AddDbContext<SqliteContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SqliteContext>()
    .AddDefaultTokenProviders();
    
builder.Services.AddIdentityServer(opt => {
    // opt.Events.RaiseErrorEvents = true;
    // opt.Events.RaiseInformationEvents = true;
    // opt.Events.RaiseFailureEvents = true;
    // opt.Events.RaiseSuccessEvents = true;
    opt.EmitStaticAudienceClaim = true;
})
.AddInMemoryApiScopes(ApplicationConfig.ApiScopes)
.AddInMemoryClients(ApplicationConfig.Clients)
.AddInMemoryIdentityResources(ApplicationConfig.IdentityResources)
.AddAspNetIdentity<ApplicationUser>()
.AddDeveloperSigningCredential();

builder.Services.AddScoped<AppService>();
builder.Services.AddScoped<IProfileService, ProfileService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

using var scope = app.Services.CreateScope();

var appService = scope.ServiceProvider.GetService<AppService>();

appService?.CreateRoles().Wait();
appService?.CreateUsers().Wait();

app.UseStaticFiles();


app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
