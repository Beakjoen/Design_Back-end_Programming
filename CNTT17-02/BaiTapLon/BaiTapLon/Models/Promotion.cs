using System.ComponentModel.DataAnnotations;

namespace BaiTapLon.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Mã khuyến mãi là bắt buộc")]
        [StringLength(20, ErrorMessage = "Mã khuyến mãi không được quá 20 ký tự")]
        public string Code { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tên khuyến mãi là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên khuyến mãi không được quá 200 ký tự")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Mô tả không được quá 500 ký tự")]
        public string? Description { get; set; }
        
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal MaxDiscountAmount { get; set; }
        public int MaxUsage { get; set; }
        public int UsedCount { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
} 