using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FeedR.Feeds.News.Messages;
using FeedR.Shared.Streaming;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Feedr.Feeds.News.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
public class ApiTests
{

    [Fact]
    public async Task get_base_endpoint_should_return_ok_status_code_and_service_name()
    {
        var response = await _client.GetAsync("/");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.ShouldBe("FeedR News feed");
    }

    [Fact]
    public async Task post_news_should_return_accepted_status_code_and_publish_news_published_event()
    {
        var tcs = new TaskCompletionSource<NewsPublished>();
        var subscriber = _app.Services.GetRequiredService<IStreamSubscriber>();
        await subscriber.SubscribeAsync<NewsPublished>("news", message =>
        {
            tcs.SetResult(message);
        });
        
        var request = new PublishNews("test news", "test category");
        var response = await _client.PostAsJsonAsync("news", request);
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var @event = await tcs.Task;
        @event.Title.ShouldBe(request.Title);
        @event.Category.ShouldBe(request.Category);
    }

    #region Arrange
    
    private readonly NewsTestApp _app;
    private readonly HttpClient _client;
    
    public ApiTests()
    {
        _app = new NewsTestApp();
        _client = _app.CreateClient();
    }

    #endregion
    

}