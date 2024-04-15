using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using System.Text.Json;

namespace Server.Services;

internal sealed class WeatherService(IConfiguration configuration) : Weather.WeatherBase
{
    public override async Task<WeatherReply> GetTodayWeatherAsync(Empty request, ServerCallContext context)
    { 
        using var client = new HttpClient()
        {
            BaseAddress = new Uri(@"https://api.openweathermap.org/data/2.5/", UriKind.Absolute),
            Timeout = TimeSpan.FromSeconds(5)
        };
        var response = await client.GetAsync(new Uri(@$"weather?q=Samara&units=metric&lang=ru&appid={configuration["ServicesApiKeys:Weather"]}", UriKind.Relative));

        var result = response.EnsureSuccessStatusCode();
        var weather = JsonSerializer.Deserialize<OpenWeatherMap.Weather>(await result.Content.ReadAsStringAsync());
        return new WeatherReply()
        {
            Temp = weather.Temperature.Value.ToString(),
            Desc = weather.DayForecast[0].Value
        };
    }
}