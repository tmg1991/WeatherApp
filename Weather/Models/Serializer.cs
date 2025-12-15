using System.Collections.ObjectModel;
using System.Text.Json;

namespace Weather.Models
{
    public class Serializer
    {
        public string Serialize(ObservableCollection<City> cities)
        {
            string json = JsonSerializer.Serialize(cities);
            return json;
        }

        public ObservableCollection<City> Deserialize(string json) 
        {
            var collection = JsonSerializer.Deserialize<ObservableCollection<City>>(json);
            return collection;
        }
    }
}
