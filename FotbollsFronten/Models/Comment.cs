using Microsoft.AspNetCore.Identity;

namespace FotbollsFronten.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    
    }
}
