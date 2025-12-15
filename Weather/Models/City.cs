namespace Weather.Models
{
    public class City
    {
        public string Name { get; init; }
        public GeoLocation Geolocation { get; init; }

        public City(string name, GeoLocation geolocation)
        {
            Name = name;
            Geolocation = geolocation;
        }
    }
}
