using Duende.IdentityServer.Configuration;
using Lojinha.IdentityServer.Auth.Config;
using Lojinha.IdentityServer.Auth.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer();

builder.Services.AddDbContext<SqliteContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<SqliteContext>();

// builder.Services.AddIdentityServer(opt => {
//     opt.Events.RaiseErrorEvents = true;
//     opt.Events.RaiseInformationEvents = true;
//     opt.Events.RaiseFailureEvents = true;
//     opt.Events.RaiseSuccessEvents = true;
//     opt.EmitStaticAudienceClaim = true;
// })
// .AddInMemoryApiScopes(ApplicationConfig.ApiScopes)
// .AddInMemoryClients(ApplicationConfig.Clients)
// .AddInMemoryIdentityResources(ApplicationConfig.IdentityResources)
// .AddAspNetIdentity<ApplicationUser>()
// .AddDeveloperSigningCredential();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
