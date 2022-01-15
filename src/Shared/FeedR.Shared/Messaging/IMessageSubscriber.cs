namespace FeedR.Shared.Messaging;

public interface IMessageSubscriber
{
    Task SubscribeAsync<T>(string topic, Action<T> handler) where T : IMessage;
}