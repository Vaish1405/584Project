namespace _584Project.Server.Dtos
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Position { get; set; } = null!;
        public int LocationId { get; set; }
        public string RestaurantName { get; set; } = null!;
        public string CityName { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
