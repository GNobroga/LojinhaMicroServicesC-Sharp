
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRegisterMaps();

builder.Services.AddRepositoriesWithServices();

builder.Services.AddSwaggerGen(opt =>
    opt.SwaggerDoc("v1", new OpenApiInfo {
        Title = "API Product",
        Description = "API to manage products"
    })
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.Authority = builder.Configuration["ServiceUrls:IdentityServer"];
        opt.TokenValidationParameters = new() {
            ValidateAudience = false
        };
        opt.RequireHttpsMetadata = false;
    });

// This service allows adding a custom policy and using it within the authorize attribute. (by GABRIEL)
builder.Services.AddAuthorization(opt => {
    opt.AddPolicy("ApiScope", policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "web");
    });
});

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
