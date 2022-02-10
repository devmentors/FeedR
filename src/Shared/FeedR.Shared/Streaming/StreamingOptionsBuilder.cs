using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Streaming;

public sealed class StreamingOptionsBuilder
{
    public IServiceCollection Services { get; }
    public StreamingOptionsBuilder(IServiceCollection services)
    {
        Services = services;
    }
}