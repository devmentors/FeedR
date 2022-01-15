using FeedR.Feeds.Weather.Models;

namespace FeedR.Feeds.Weather.Services;

internal interface IWeatherFeed
{
    IAsyncEnumerable<WeatherData> SubscribeAsync(string location, CancellationToken cancellationToken);
}