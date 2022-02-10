using FeedR.Shared.Serialization;
using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using StackExchange.Redis;

namespace FeedR.Shared.Redis.Streaming
{
    public static class StreamingOptionsBuilderExtensions
    {
        /// <summary>
        ///     Register and replace redis stream implementations for <see cref="IStreamPublisher"/> and <see cref="IStreamSubscriber"/>.
        /// </summary>
        /// <remarks>
        ///     Tries to register singleton service <see cref="SystemTextJsonSerializer"/> as <see cref="ISerializer"/> as it is a prerequisite.
        /// </remarks>
        /// <param name="builder"></param>
        /// <returns>
        ///     <see cref="StreamingOptionsBuilder"/> instance for chained method calls.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     If the redis core services are not registered
        ///     (via calling .AddRedis() on <see cref="IServiceCollection"/>) before using redis streams
        /// </exception>
        public static StreamingOptionsBuilder UseRedisStreaming(this StreamingOptionsBuilder builder)
        {
            var muxerType = typeof(IConnectionMultiplexer);
            if (!builder.Services.Any(sd => sd.ServiceType == muxerType))
            {
                throw new InvalidOperationException("Cannot use redis streaming without regsitering redis services first." +
                    "Make sure you have added a call to .AddRedis() before using redis streaming.");
            }

            builder.Services.TryAddSingleton<ISerializer, SystemTextJsonSerializer>();

            builder.Services
                .RemoveAll<IStreamPublisher>()
                .RemoveAll<IStreamSubscriber>()
                .AddSingleton<IStreamPublisher, RedisStreamPublisher>()
                .AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();

            return builder;
        }
    }
}
