using FeedR.Notifier.Events.External;
using FeedR.Shared.Messaging;

namespace FeedR.Notifier.Services;

internal sealed class NotifierMessagingBackgroundService : BackgroundService
{
    private readonly IMessageSubscriber _messageSubscriber;
    private readonly ILogger<NotifierMessagingBackgroundService> _logger;

    public NotifierMessagingBackgroundService(IMessageSubscriber messageSubscriber, 
        ILogger<NotifierMessagingBackgroundService> logger)
    {
        _messageSubscriber = messageSubscriber;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageSubscriber.SubscribeAsync<OrderPlaced>("orders", messageEnvelope =>
        {
            var correlationId = messageEnvelope.CorrelationId;
            _logger.LogInformation($"Order with ID: '{messageEnvelope.Message.OrderId}' for symbol: '{messageEnvelope.Message.Symbol}' has been placed. " +
                                   $"Correlation ID: '{correlationId}'.");
        });
        
        return Task.CompletedTask;
    }
}