namespace _584Project.Server.Dtos

{
    public class LocationReviewDto
    {
        public int LocationId { get; set; }

        public string CityName { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string AdminName { get; set; } = null!;

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string RestaurantName { get; set; } = null!;

        public float? ReviewScore { get; set; }

        public string? ReviewCount { get; set; }
    }
}
