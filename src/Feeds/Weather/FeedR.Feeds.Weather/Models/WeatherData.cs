namespace FeedR.Feeds.Weather.Models;

internal record WeatherData(string Location, double Temperature, double Humidity, double Wind, string Condition);
