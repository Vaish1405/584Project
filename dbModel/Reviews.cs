using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dbModel;

[Table("reviews")]
public class Review
{
    [Key]
    [Column("review_id")]
    public int ReviewId { get; set; }

    [Column("location_id")]
    public int LocationId { get; set; }

    [Column("review_score")]
    public float ReviewScore { get; set; }

    [Column("review_count")]
    [StringLength(10)]
    public string ReviewCount { get; set; } = null!;
}
