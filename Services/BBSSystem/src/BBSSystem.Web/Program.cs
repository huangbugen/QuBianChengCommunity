using BBSSystem.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAutofac();
builder.Services.AddApplication<BBSSystemWebModule>();

var app = builder.Build();

app.InitializeApplication();

app.MapControllers();

app.Run();