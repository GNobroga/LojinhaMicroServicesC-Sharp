using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ItemAPI", opt => {
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ItemAPI"]!);
});

builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddAuthentication(opt => {
    opt.DefaultScheme = "Cookies";
    opt.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies", opt => opt.ExpireTimeSpan = TimeSpan.FromMinutes(10))
.AddOpenIdConnect("oidc", opt => {
    opt.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
    opt.GetClaimsFromUserInfoEndpoint = true;
    opt.ClientId = "lojinha_web";
    opt.ClientSecret = "lojinha_super_secret";
    opt.ResponseType = "code";
    opt.ClaimActions.MapJsonKey("role", "role", "role");
    opt.ClaimActions.MapJsonKey("sub", "sub", "sub");
    opt.TokenValidationParameters.NameClaimType = "name";
    opt.TokenValidationParameters.RoleClaimType = "role";
    opt.Scope.Add("web");
    opt.SaveTokens = true;
    opt.RequireHttpsMetadata = false;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=List}/{id:int?}"
);

app.Run();
