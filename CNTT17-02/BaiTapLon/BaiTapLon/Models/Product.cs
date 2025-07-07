using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTapLon.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageUrls { get; set; } // Chuỗi JSON hoặc phân tách bằng dấu phẩy
        public string? TechnicalSpecs { get; set; } // Chuỗi JSON hoặc HTML
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int Stock { get; set; }
        public string? Status { get; set; } // Còn hàng, Hết hàng, Ngừng kinh doanh
        public bool IsFeatured { get; set; } = false;
        public bool IsBestSeller { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public double AverageRating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;
        public int DisplayOrder { get; set; } = 0;
        public int SoldCount { get; set; } = 0;
        public int? SalePercent { get; set; } // % giảm giá đặc biệt, ví dụ 40
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
} 