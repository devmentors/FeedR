using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Redis.Streaming;

public static class Extensions
{
    [Obsolete("Use .AddStreaming() with `StreamingOptionsBuilder` action configurator instead.")]
    public static IServiceCollection AddRedisStreaming(this IServiceCollection services)
    {
        return services.AddStreaming(options => options.UseRedisStreaming());
    }
}