using FeedR.Aggregator.Services.Models;
using FeedR.Shared.Streaming;

namespace FeedR.Aggregator.Services;

internal sealed class PricingStreamBackgroundService : BackgroundService
{
    private readonly IStreamSubscriber _subscriber;
    private readonly IPricingHandler _pricingHandler;
    private readonly ILogger<PricingStreamBackgroundService> _logger;

    public PricingStreamBackgroundService(IStreamSubscriber subscriber, IPricingHandler pricingHandler,
        ILogger<PricingStreamBackgroundService> logger)
    {
        _subscriber = subscriber;
        _pricingHandler = pricingHandler;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriber.SubscribeAsync<CurrencyPair>("pricing", currencyPair =>
        {
            _logger.LogInformation($"Pricing '{currencyPair.Symbol}' = {currencyPair.Value:F}, " +
                                   $"timestamp: {currencyPair.Timestamp}");
            _ = _pricingHandler.HandleAsync(currencyPair);
        });
    }
}