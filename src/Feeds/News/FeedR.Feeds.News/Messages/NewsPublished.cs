using FeedR.Shared.Messaging;

namespace FeedR.Feeds.News.Messages;

public record NewsPublished(string Title, string Category) : IMessage;