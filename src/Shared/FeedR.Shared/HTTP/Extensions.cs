using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace FeedR.Shared.HTTP;

public static class Extensions
{
    public static IServiceCollection AddHttpApiClient<TInterface, TClient>(this IServiceCollection services) 
        where TInterface : class where TClient : class, TInterface
    {
        services
            .AddHttpClient<TInterface, TClient>()
            .AddPolicyHandler(GetPolicy());
        
        return services;

        static IAsyncPolicy<HttpResponseMessage> GetPolicy()
            => HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.BadRequest)
                .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)));
    }
}