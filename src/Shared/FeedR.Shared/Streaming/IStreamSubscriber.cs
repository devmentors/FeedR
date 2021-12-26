namespace FeedR.Shared.Streaming;

public interface IStreamSubscriber
{
    Task SubscribeAsync<T>(string topic, Action<T> handler) where T : class;
}