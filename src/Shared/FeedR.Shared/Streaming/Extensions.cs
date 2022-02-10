using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Streaming;

public static class Extensions
{
    /// <summary>
    /// Add streaming with NO-OP <see cref="IStreamPublisher"/> and <see cref="IStreamSubscriber"/> implementations.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>same <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddStreaming(this IServiceCollection services)
        => services
                   .AddSingleton<IStreamPublisher, DefaultStreamPublisher>()
                   .AddSingleton<IStreamSubscriber, DefaultStreamSubscriber>();


    /// <summary>
    ///     Add streaming using the <paramref name="optionsAction"/>
    ///     to register the <see cref="IStreamPublisher"/> and <see cref="IStreamSubscriber"/> implementations
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection AddStreaming(this IServiceCollection services, Action<StreamingOptionsBuilder> optionsAction)
    {
        if (optionsAction is null)
        {
            throw new ArgumentNullException(nameof(optionsAction));
        }

        var optionsBuilder = new StreamingOptionsBuilder(services);
        optionsAction(optionsBuilder);
        return services;
    }
}