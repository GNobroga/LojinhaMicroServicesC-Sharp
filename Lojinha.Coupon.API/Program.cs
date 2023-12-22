using Lojinha.Coupon.API.Context;
using Lojinha.Coupon.API.Entities;
using Lojinha.Coupon.API.ObjectValues;
using Lojinha.Coupon.API.Repositores;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(opt => {
    opt.CreateMap<Coupon, CouponVO>().ReverseMap();
});

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
