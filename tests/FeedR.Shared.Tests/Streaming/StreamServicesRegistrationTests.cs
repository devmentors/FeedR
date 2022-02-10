using System;
using FeedR.Shared.Redis;
using FeedR.Shared.Redis.Streaming;
using FeedR.Shared.Streaming;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FeedR.Shared.Tests.Streaming
{
    public class StreamServicesRegistrationTests
    {

        [Fact]
        public void RegisterStreaming_Standalone_Adds_DefaultImplementation()
        {
            var sp = new ServiceCollection()
                .AddStreaming()
                .BuildServiceProvider();

            Assert.Single(sp.GetServices<IStreamPublisher>());
            Assert.Single(sp.GetServices<IStreamSubscriber>());

            Assert.IsType<DefaultStreamPublisher>(sp.GetRequiredService<IStreamPublisher>());
            Assert.IsType<DefaultStreamSubscriber>(sp.GetRequiredService<IStreamSubscriber>());
        }

        [Fact]
        public void RegisterStreaming_Redis_Adds_RedisImplementations()
        {
            var configuraiton = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var sp = new ServiceCollection()
                .AddRedis(configuraiton)
                .AddStreaming(options => options.UseRedisStreaming())
                .BuildServiceProvider();

            Assert.Single(sp.GetServices<IStreamPublisher>());
            Assert.Single(sp.GetServices<IStreamSubscriber>());

            Assert.IsType<RedisStreamPublisher>(sp.GetRequiredService<IStreamPublisher>());
            Assert.IsType<RedisStreamSubscriber>(sp.GetRequiredService<IStreamSubscriber>());
        }

        [Fact]
        public void Obsolete_RegisterStreaming_Redis_Adds_RedisImplementations()
        {
            var configuraiton = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var sp = new ServiceCollection()
                .AddRedis(configuraiton)
                .AddStreaming()
                .AddRedisStreaming()
                .BuildServiceProvider();

            Assert.Single(sp.GetServices<IStreamPublisher>());
            Assert.Single(sp.GetServices<IStreamSubscriber>());

            Assert.IsType<RedisStreamPublisher>(sp.GetRequiredService<IStreamPublisher>());
            Assert.IsType<RedisStreamSubscriber>(sp.GetRequiredService<IStreamSubscriber>());
        }

        [Fact]
        public void RegisterStreaming_Redis_WithoutCoreRedisServices_Throws()
        {
            var configuraiton = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var exception = Assert.Throws<InvalidOperationException>(() =>
                                new ServiceCollection()
                                        .AddStreaming(options => options.UseRedisStreaming())
                                        .BuildServiceProvider());
        }
    }

}
