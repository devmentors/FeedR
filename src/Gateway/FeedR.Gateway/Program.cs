using System.Diagnostics;
using FeedR.Shared.Observability;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(transform =>
        {
            var activity = Activity.Current;
            var correlationId = Guid.NewGuid().ToString("N");
            transform.ProxyRequest.Headers.AddCorrelationId(correlationId);

            return ValueTask.CompletedTask;
        });
    });
    
var app = builder.Build();

app.MapGet("/", () => "FeedR Gateway");
app.MapReverseProxy();

app.Run();