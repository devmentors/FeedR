using FeedR.Shared.Messaging;

namespace FeedR.Aggregator.Events;

internal record OrderPlaced(string OrderId, string Symbol) : IMessage;