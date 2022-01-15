namespace FeedR.Shared.Messaging;

internal sealed class DefaultMessageSubscriber : IMessageSubscriber
{
    public Task SubscribeAsync<T>(string topic, Action<T> handler) where T : IMessage => Task.CompletedTask;
}