using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class PricingStreamBackgroundService : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly ILogger<PricingStreamBackgroundService> _logger;

    public PricingStreamBackgroundService(IStreamSubscriber subscriber, ILogger<PricingStreamBackgroundService> logger)
    {
        _subscriber = subscriber;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair =>
        {
            _logger.LogInformation($"Pricing '{currencyPair.Symbol}' = {currencyPair.Value:F}, " +
                                   $"timestamp: {currencyPair.Timestamp}");
        });
    }
    
    private record CurrencyPair(string Symbol, decimal Value, long Timestamp);
}