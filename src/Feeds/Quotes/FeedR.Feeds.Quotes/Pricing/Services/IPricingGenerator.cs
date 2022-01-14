using FeedR.Feeds.Quotes.Pricing.Models;

namespace FeedR.Feeds.Quotes.Pricing.Services;

internal interface IPricingGenerator
{
    IEnumerable<string> GetSymbols();
    IAsyncEnumerable<CurrencyPair> StartAsync();
    Task StopAsync();
    event EventHandler<CurrencyPair>? PricingUpdated;
}