using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModel;

[Table("staff")]
public class Staff
{
    [Key]
    [Column("staff_id")]
    public int StaffId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    public string LastName { get; set; } = null!;

    [Column("position")]
    public string Position { get; set; } = null!;

    [Column("location_id")]
    public int LocationId { get; set; }
}
