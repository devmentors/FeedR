using FeedR.Feeds.Quotes.Pricing.Requests;
using FeedR.Feeds.Quotes.Pricing.Services;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddStreaming()
    .AddSerialization()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming()
    .AddSingleton<PricingRequestsChannel>()
    .AddSingleton<IPricingGenerator, PricingGenerator>()
    .AddHostedService<PricingBackgroundService>();

var app = builder.Build();

app.MapGet("/", () => "FeedR Quotes feed");

app.MapPost("/pricing/start", async (PricingRequestsChannel channel) =>
{
    await channel.Requests.Writer.WriteAsync(new StartPricing());
    return Results.Ok();
});

app.MapPost("/pricing/stop", async (PricingRequestsChannel channel) =>
{
    await channel.Requests.Writer.WriteAsync(new StopPricing());
    return Results.Ok();
});

app.Run();