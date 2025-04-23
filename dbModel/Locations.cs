using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModel;

[Table("locations")]
public class Location
{
    [Key]
    [Column("location_id")]
    public int LocationId { get; set; }

    [Column("city_name")]
    public string CityName { get; set; } = null!;

    [Column("country")]
    public string Country { get; set; } = null!;

    [Column("admin_name")]
    public string AdminName { get; set; } = null!;

    [Column("latitude")]
    public float Latitude { get; set; }

    [Column("longitude")]
    public float Longitude { get; set; }

    [Column("restaurant_name")]
    public string RestaurantName { get; set; } = null!;
}
