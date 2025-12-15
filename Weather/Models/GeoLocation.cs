namespace Weather.Models
{
    public class GeoLocation
    {
        public double Lon { get; init; }
        public double Lat { get; init; }

        public GeoLocation(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }
    }
}
