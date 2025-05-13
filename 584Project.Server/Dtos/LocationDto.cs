namespace _584Project.Server.Dtos
{
    public class LocationDto
    {
        public int LocationId { get; set; }
        public string CityName { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string AdminName { get; set; } = null!;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string RestaurantName { get; set; } = null!;
    }
}
