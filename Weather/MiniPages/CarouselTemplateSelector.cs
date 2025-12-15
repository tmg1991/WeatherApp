using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.MiniPages
{
    public class CarouselTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CurrentWeatherTemplate { get; set; }
        public DataTemplate TemperatureAndPrecipitationForecastTemplate { get; set; }
        public DataTemplate WindForecastTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item switch
            {
                TemperatureAndPrecipitationForecastItem => TemperatureAndPrecipitationForecastTemplate,
                CurrentWeatherItem => CurrentWeatherTemplate,
                WindForecastItem => WindForecastTemplate,
                _ => null
            };
        }
    }
}
