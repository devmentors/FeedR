namespace FeedR.Shared.Streaming;

internal sealed class DefaultStreamPublisher : IStreamPublisher
{
    public Task PublishAsync<T>(string topic, T data) where T : class => Task.CompletedTask;
}