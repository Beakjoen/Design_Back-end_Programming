namespace BaiTapLon.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
        
        // Navigation properties
        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
} 