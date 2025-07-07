using System;

namespace BaiTapLon.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVisible { get; set; } = true;
        public string? ImageUrls { get; set; } // Chuỗi JSON hoặc phân tách bằng dấu phẩy
    }
} 