using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Redis.Streaming;

public static class Extensions
{
    public static IServiceCollection AddRedisStreaming(this IServiceCollection services)
        => services
            .AddSingleton<IStreamPublisher, RedisStreamPublisher>()
            .AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();
}