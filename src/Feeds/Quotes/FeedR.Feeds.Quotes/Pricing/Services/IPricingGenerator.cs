using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal interface IPricingGenerator
{
    IAsyncEnumerable<CurrencyPair> StartAsync();
    Task StopAsync();
}