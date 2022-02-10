using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace FeedR.Shared.Redis;

public static class Extensions
{
    /// <summary>
    /// Adds Redis to <see cref="IServiceCollection"/>.
    /// </summary>
    /// <remarks>
    /// Uses the lower-level <see cref="ServiceDescriptor"/> APIs to acquire an instance of <see cref="IConfiguration"/>
    /// without the need of passing it in explicitly
    /// </remarks>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        var serviceDescriptor = ServiceDescriptor.Singleton<IConnectionMultiplexer>(sp =>
        {
            var options = sp.GetRequiredService<IConfiguration>().GetRequiredSection("redis").Get<RedisOptions>();
            return ConnectionMultiplexer.Connect(options.ConnectionString);
        });

        services.Add(serviceDescriptor);

        return services;
    }

    /// <summary>
    /// Adds Redis to <see cref="IServiceCollection"/>.
    /// </summary>
    /// <returns>The same <see cref="IServiceCollection"/> instance.</returns>
    [Obsolete("Use the overload that does not take the IConfiguration explicitely")]
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        return AddRedis(services);
    }
}