namespace BaiTapLon.Models
{
    public class Banner
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
    }
} 