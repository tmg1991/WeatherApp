using System.Text.Json.Serialization;

namespace Weather.Models
{
   
    public class OpenMeteoResponse
    {
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("current")]
        public CurrentWeather Current { get; set; }

        [JsonPropertyName("hourly")]
        public HourlyWeather Hourly { get; set; }

        [JsonPropertyName("daily")]
        public DailyWeather Daily { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("temperature_2m")]
        public double Temperature { get; set; }

        [JsonPropertyName("relative_humidity_2m")]
        public int Humidity { get; set; }

        [JsonPropertyName("precipitation")]
        public double Precipitation { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double WindSpeed { get; set; }

        [JsonPropertyName("wind_direction_10m")]
        public int WindDirection { get; set; }

        [JsonPropertyName("wind_gusts_10m")]
        public double WindGusts { get; set; }
    }

    public class HourlyWeather
    {
        [JsonPropertyName("time")]
        public string[] Time { get; set; }

        [JsonPropertyName("temperature_2m")]
        public double[] Temperature { get; set; }

        [JsonPropertyName("precipitation_probability")]
        public double[] PrecipitationProbability { get; set; }

        [JsonPropertyName("wind_speed_10m")]
        public double[] WindSpeed { get; set; }

        [JsonPropertyName("wind_gusts_10m")]
        public double[] WindGust { get; set; }
    }

    public class DailyWeather
    {
        [JsonPropertyName("temperature_2m_max")]
        public double[] TempMax { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public double[] TempMin { get; set; }

        [JsonPropertyName("uv_index_max")]
        public double[] UvMax { get; set; }

        [JsonPropertyName("precipitation_probability_max")]
        public double[] PrecipitationProbabilityMax { get; set; }
    }

}
