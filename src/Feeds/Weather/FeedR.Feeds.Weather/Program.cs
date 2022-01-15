using FeedR.Feeds.Weather.Services;
using FeedR.Shared.HTTP;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services
    .AddSerialization()
    .AddStreaming()
    .AddRedis(builder.Configuration)
    .AddRedisStreaming()
    .AddHostedService<WeatherBackgroundService>()
    .AddHttpClient()
    .AddHttpApiClient<IWeatherFeed, WeatherFeed>();

var app = builder.Build();

app.MapGet("/", () => "FeedR Weather feed");

app.Run();