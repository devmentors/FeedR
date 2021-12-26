namespace FeedR.Feeds.Quotes.Pricing.Services;

internal interface IPricingGenerator
{
    Task StartAsync();
    Task StopAsync();
}