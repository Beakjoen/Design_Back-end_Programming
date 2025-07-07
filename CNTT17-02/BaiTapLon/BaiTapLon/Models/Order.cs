using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaiTapLon.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ShippingAddress { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public string? PaymentMethod { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? PromotionCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = new();
        public string? Status { get; set; }
    }
} 