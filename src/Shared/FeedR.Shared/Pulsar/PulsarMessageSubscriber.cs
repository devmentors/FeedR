using System.Reflection;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using FeedR.Shared.Messaging;
using FeedR.Shared.Serialization;
using Microsoft.Extensions.Logging;
using IMessage = FeedR.Shared.Messaging.IMessage;

namespace FeedR.Shared.Pulsar;

internal sealed class PulsarMessageSubscriber : IMessageSubscriber
{
    private readonly ISerializer _serializer;
    private readonly ILogger<PulsarMessageSubscriber> _logger;
    private readonly IPulsarClient _client;
    private readonly string _consumerName;

    public PulsarMessageSubscriber(ISerializer serializer, ILogger<PulsarMessageSubscriber> logger)
    {
        _serializer = serializer;
        _logger = logger;
        _client = PulsarClient.Builder().Build();
        _consumerName = Assembly.GetEntryAssembly()?.FullName?.Split(",")[0].ToLowerInvariant() ?? string.Empty;
    }
    
    public async Task SubscribeAsync<T>(string topic, Action<T> handler) where T : class, IMessage
    {
        var subscription = $"{_consumerName}_{topic}";
        var consumer = _client.NewConsumer()
            .SubscriptionName(subscription)
            .Topic($"persistent://public/default/{topic}")
            .Create();

        await foreach (var message in consumer.Messages())
        {
            var producer = message.Properties["producer"];
            var customId = message.Properties["custom_id"];
            _logger.LogInformation($"Received a message with ID: '{message.MessageId}' from: '{producer}' " +
                                   $"with custom ID: '{customId}'.");
            var payload = _serializer.DeserializeBytes<T>(message.Data.FirstSpan.ToArray());
            if (payload is not null)
            {
                var json = _serializer.Serialize(payload);
                _logger.LogInformation(json);
                handler(payload);
            }

            await consumer.Acknowledge(message);
        }
    }
}