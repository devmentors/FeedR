using Microsoft.Extensions.DependencyInjection;

namespace FeedR.Shared.Serialization;

public static class Extensions
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
        => services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
}