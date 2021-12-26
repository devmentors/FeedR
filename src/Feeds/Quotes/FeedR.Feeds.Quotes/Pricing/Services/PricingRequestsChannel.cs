using System.Threading.Channels;
using FeedR.Feeds.Quotes.Pricing.Requests;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal sealed class PricingRequestsChannel
{
    public readonly Channel<IPricingRequest> Requests = Channel.CreateUnbounded<IPricingRequest>();
}