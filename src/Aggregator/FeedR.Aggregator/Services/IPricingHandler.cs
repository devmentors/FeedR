using FeedR.Aggregator.Services.Models;

namespace FeedR.Aggregator.Services;

internal interface IPricingHandler
{
    Task HandleAsync(CurrencyPair currencyPair);
}