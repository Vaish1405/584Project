namespace _584Project.Server.Dtos
{
    public class ReviewDto
    {
        public int ReviewId { get; set; }
        public int LocationId { get; set; }
        public float ReviewScore { get; set; }
        public string ReviewCount { get; set; } = null!;
    }
}
