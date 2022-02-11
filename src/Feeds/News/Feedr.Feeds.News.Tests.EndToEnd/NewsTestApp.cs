using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Feedr.Feeds.News.Tests.EndToEnd;

[ExcludeFromCodeCoverage]
internal sealed class NewsTestApp : WebApplicationFactory<Program>
{
}