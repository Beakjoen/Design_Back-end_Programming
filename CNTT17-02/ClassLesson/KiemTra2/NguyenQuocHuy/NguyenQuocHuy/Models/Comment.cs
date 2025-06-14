using System.Runtime.InteropServices;

namespace NguyenQuocHuy.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }
        public User? User { get; set; }
    }
}
