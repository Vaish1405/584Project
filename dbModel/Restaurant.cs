using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dbModel;

[Keyless]
public partial class Restaurant
{
    [Column("city_id")]
    public int CityId { get; set; }

    [Column("restaurant_id")]
    public int RestaurantId { get; set; }

    [Column("restaurant_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string RestaurantName { get; set; } = null!;

    [Column("review_score")]
    public double ReviewScore { get; set; }

    [Column("review_count")]
    public int ReviewCount { get; set; }
}
