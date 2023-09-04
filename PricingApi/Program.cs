using PricingApi.Contracts;
using PricingApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPricingService,PricingService>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/", () =>
{
    using (var scoped = ser)
    {
        
    }
});

app.Run();