using Lojinha.Consumer.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ItemAPI", opt => {
    opt.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ItemAPI"]!);
});

builder.Services.AddScoped<IItemService, ItemService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Items}/{action=List}/{id:int?}"
);

app.Run();
