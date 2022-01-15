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
        _messageSubscriber.SubscribeAsync<OrderPlaced>("orders", message =>
        {
            _logger.LogInformation($"Order with ID: '{message.OrderId}' for symbol: '{message.Symbol}' has been placed.");
        });
        
        return Task.CompletedTask;
    }
}