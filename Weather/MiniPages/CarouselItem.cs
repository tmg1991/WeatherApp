using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.MiniPages
{
    public abstract class CarouselItem
    {
    }

    public class CurrentWeatherItem : CarouselItem
    {
        public double Temperature { get; set; }
        public double RelativeHumidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
    }

    public class TemperatureAndPrecipitationForecastItem : CarouselItem
    {
        public List<double> TemperatureForecast { get; set; }
        public List<double> PrecipitationForecast { get; set; }
    }

    public class WindForecastItem : CarouselItem
    {
        public List<double> WindSpeedForecast { get; set; }
        public List<double> WindGustForecast { get; set; }
    }
}
