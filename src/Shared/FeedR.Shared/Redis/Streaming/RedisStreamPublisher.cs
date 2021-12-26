using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;
using StackExchange.Redis;

namespace FeedR.Shared.Redis.Streaming;

internal sealed class RedisStreamPublisher : IStreamPublisher
{
    private readonly ISerializer _serializer;
    private readonly ISubscriber _subscriber;

    public RedisStreamPublisher(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
    {
        _serializer = serializer;
        _subscriber = connectionMultiplexer.GetSubscriber();
    }
    
    public Task PublishAsync<T>(string topic, T data) where T : class
    {
        var payload = _serializer.Serialize(data);
        return _subscriber.PublishAsync(topic, payload);
    }
}