using FeedR.Notifier.Services;
using FeedR.Shared.Messaging;
using FeedR.Shared.Observability;
using FeedR.Shared.Pulsar;
using FeedR.Shared.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()
    .AddSerialization()
    .AddMessaging()
    .AddPulsar()
    .AddHostedService<NotifierMessagingBackgroundService>();

var app = builder.Build();
app.UseCorrelationId();

app.MapGet("/", () => "FeedR Notifier");

app.Run();