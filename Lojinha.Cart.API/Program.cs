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

app.UseCors(cors => cors.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

app.MapControllers();

app.Run();
