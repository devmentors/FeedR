using FeedR.Shared.Messaging;

namespace FeedR.Notifier.Events.External;

internal record OrderPlaced(string OrderId, string Symbol) : IMessage;