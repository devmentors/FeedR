namespace FeedR.Shared.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(string topic, T message) where T : IMessage;
}