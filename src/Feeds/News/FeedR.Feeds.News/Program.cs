using FeedR.Feeds.News.Messages;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddStreaming()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming()
    .AddSerialization();

var app = builder.Build();

app.MapGet("/", () => "FeedR News feed");

app.MapPost("/news", async (PublishNews news, IStreamPublisher streamPublisher) =>
{
    //TODO: Handle the published news
    var @event = new NewsPublished(news.Title, news.Category);
    await streamPublisher.PublishAsync("news", @event);
    
    // For simple "background task simulation" testing purposes
    // Task.Run(() => Task.Delay(1000)).ContinueWith(t => streamPublisher.PublishAsync("news", @event));
    return Results.Accepted();
});

app.Run();