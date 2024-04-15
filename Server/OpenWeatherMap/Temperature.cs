using System.Text.Json.Serialization;

namespace Server.OpenWeatherMap;

internal readonly struct Temperature
{
    [JsonPropertyName("temp")]
    public float Value { get; init; }

    [JsonPropertyName("feels_like")]
    public float FeelsLikeValue { get; init; }

    [JsonConstructor]
    public Temperature(float value, float feelsLikeValue)
    {
        Value = value;
        FeelsLikeValue = feelsLikeValue;
    }
}
