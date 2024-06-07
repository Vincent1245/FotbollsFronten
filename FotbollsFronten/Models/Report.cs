namespace FotbollsFronten.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string UserId { get; set; }
        public int? CommentId { get; set; }
        public int? PostId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}

