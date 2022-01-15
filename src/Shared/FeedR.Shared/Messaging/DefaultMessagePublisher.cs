namespace FeedR.Shared.Messaging;

internal sealed class DefaultMessagePublisher : IMessagePublisher
{
    public Task PublishAsync<T>(string topic, T message) where T : IMessage => Task.CompletedTask;
}