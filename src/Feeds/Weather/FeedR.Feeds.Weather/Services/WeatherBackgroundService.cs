using FeedR.Shared.Streaming;

namespace FeedR.Feeds.Weather.Services;

internal sealed class WeatherBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStreamPublisher _streamPublisher;
    private readonly ILogger<WeatherBackgroundService> _logger;

    public WeatherBackgroundService(IServiceProvider serviceProvider, IStreamPublisher streamPublisher,
        ILogger<WeatherBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _streamPublisher = streamPublisher;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var weatherFeed = scope.ServiceProvider.GetRequiredService<IWeatherFeed>(); 
        await foreach (var weather in weatherFeed.SubscribeAsync("Cracow", stoppingToken))
        {
            _logger.LogInformation($"{weather.Location}: {weather.Temperature} C, {weather.Humidity} %," +
                                   $"{weather.Wind} km/h [{weather.Condition}]");
            await _streamPublisher.PublishAsync("weather", weather);
        }
    }
}