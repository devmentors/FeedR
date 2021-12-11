var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "FeedR Notifier");

app.Run();