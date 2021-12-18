using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"));
    
var app = builder.Build();

app.MapGet("/", () => "FeedR Gateway");
app.MapReverseProxy();

app.Run();