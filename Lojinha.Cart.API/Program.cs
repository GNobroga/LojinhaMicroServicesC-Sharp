using Lojinha.Cart.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAPIServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureAPIEndpoints();

app.ApiHandlerException();

app.MapControllers();

app.Run();
