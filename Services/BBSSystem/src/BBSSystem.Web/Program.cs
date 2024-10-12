using BBSSystem.Web;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAutofac();
builder.Services.AddApplication<BBSSystemWebModule>();

var app = builder.Build();

app.InitializeApplication();

app.MapGet("/heart", () =>
            {
                return new OkResult();
            });

app.MapControllers();

app.Run();