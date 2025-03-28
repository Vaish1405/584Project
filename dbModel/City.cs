using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dbModel;

[Keyless]
public partial class City
{
    [Column("country")]
    [StringLength(50)]
    [Unicode(false)]
    public string Country { get; set; } = null!;

    [Column("city")]
    [StringLength(50)]
    [Unicode(false)]
    public string City1 { get; set; } = null!;

    [Column("lat")]
    public double Lat { get; set; }

    [Column("lng")]
    public double Lng { get; set; }

    [Column("iso2")]
    [StringLength(50)]
    [Unicode(false)]
    public string Iso2 { get; set; } = null!;

    [Column("iso3")]
    [StringLength(10)]
    [Unicode(false)]
    public string Iso3 { get; set; } = null!;

    [Column("admin_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string AdminName { get; set; } = null!;

    [Column("city_id")]
    public int CityId { get; set; }
}
