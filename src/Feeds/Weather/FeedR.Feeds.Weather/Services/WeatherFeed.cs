using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using FeedR.Feeds.Weather.Models;

namespace FeedR.Feeds.Weather.Services;

internal sealed class WeatherFeed : IWeatherFeed
{
    //TODO: Move these values to dedicated api settings and options type
    private const string ApiKey = "49f6e0fc8b3a4490bf1193606212812";
    private const string ApiUrl = "https://api.weatherapi.com/v1/current.json";

    private readonly HttpClient _client;
    private readonly ILogger<WeatherFeed> _logger;

    public WeatherFeed(HttpClient client, ILogger<WeatherFeed> logger)
    {
        _client = client;
        _logger = logger;
    }
    
    public async IAsyncEnumerable<WeatherData> SubscribeAsync(string location, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var url = $"{ApiUrl}?key={ApiKey}&q={location}&aqi=no";
        while (!cancellationToken.IsCancellationRequested)
        {
            WeatherApiResponse? response;
            
            try
            {
                response = await _client.GetFromJsonAsync<WeatherApiResponse>(url, cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
            
            if (response is null)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                continue;
            }
            
            yield return new WeatherData($"{response.Location.Name}, {response.Location.Country}",
                response.Current.TempC, response.Current.Humidity, response.Current.WindKph,
                response.Current.Condition.Text);

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

    private record WeatherApiResponse(Location Location, Weather Current);

    private record Location(string Name, string Country);

    private record Condition(string Text);
    
    private record Weather([property: JsonPropertyName("temp_c")] double TempC, double Humidity, Condition Condition,
        [property: JsonPropertyName("wind_kph")] double WindKph);
}