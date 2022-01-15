using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class WeatherStreamBackgroundService : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly ILogger<WeatherStreamBackgroundService> _logger;

    public WeatherStreamBackgroundService(IStreamSubscriber subscriber, ILogger<WeatherStreamBackgroundService> logger)
    {
        _subscriber = subscriber;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<WeatherData>("weather", data =>
        {
            _logger.LogInformation($"{data.Location}: {data.Temperature} C, {data.Humidity} %," +
                                   $"{data.Wind} km/h [{data.Condition}]");
        });
    }
    
    private record WeatherData(string Location, double Temperature, double Humidity, double Wind, string Condition);
}