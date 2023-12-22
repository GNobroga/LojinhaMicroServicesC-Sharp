using System.Formats.Tar;
using Lojinha.Consumer.Web.Interceptors;
using Lojinha.Consumer.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.CookiePolicy;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ItemAPI", opt => {
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ItemAPI"]!);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient("CartAPI", opt => {
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CartAPI"]!);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddHttpClient("CouponAPI", opt => {
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]!);
}).AddHttpMessageHandler<RequestHandler>();

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ICartService, CartService>();

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
    opt.Scope.Add("profile");
    opt.Scope.Add("openid");
    opt.Scope.Add("email");
    opt.SaveTokens = true;
    opt.CallbackPath = "/home";
    opt.RequireHttpsMetadata = false; 
    opt.UsePkce = true;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RequestHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Index");
}

app.UseRouting();

app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.Always,
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id:int?}"
);

app.Run();
