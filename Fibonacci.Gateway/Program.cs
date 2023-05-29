using Fibonacci.Gateway.Domain.Models.Aggregates;
using Fibonacci.Gateway.Infrastructure.Services;
using Gateway.Extensions;
using Gateway.MessageBus.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCorsDefault();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFibonacciService, FibonacciService>();
builder.Services.AddHostedService<FibonacciConsumer>();

var app = builder.Build();

app.UseRouting();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Fibonacci}/{action=Start}");


app.Run();
