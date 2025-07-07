using Microsoft.AspNetCore.Mvc;
using BaiTapLon.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLon.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private const string CartSessionKey = "CartSession";
        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            
            // Cập nhật thông tin sản phẩm từ database
            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    item.Product = product;
                }
            }
            
            // Tính toán
            decimal subtotal = cart.Sum(item => (item.Product?.DiscountPrice ?? item.Product?.Price ?? 0) * item.Quantity);
            decimal shippingFee = CalculateShippingFee(subtotal);
            string? appliedPromotion = HttpContext.Session.GetString("AppliedPromotion");
            decimal discountAmount = 0;
            
            if (!string.IsNullOrEmpty(appliedPromotion))
            {
                var promotion = _context.Promotions.FirstOrDefault(p =>
                    p.Code == appliedPromotion &&
                    p.IsActive &&
                    p.UsedCount < p.MaxUsage &&
                    p.EndDate >= DateTime.Now.Date &&
                    subtotal >= p.MinOrderAmount
                );
                if (promotion != null)
                {
                    if (promotion.DiscountPercent > 0)
                    {
                        discountAmount = subtotal * promotion.DiscountPercent / 100;
                        if (discountAmount > promotion.MaxDiscountAmount)
                            discountAmount = promotion.MaxDiscountAmount;
                    }
                    else
                    {
                        discountAmount = promotion.DiscountAmount;
                    }
                }
            }
            
            decimal total = subtotal + shippingFee - discountAmount;
            
            ViewBag.Subtotal = subtotal;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.Total = total;
            ViewBag.AppliedPromotion = appliedPromotion;
            
            return View(cart);
        }

        [HttpPost]
        public IActionResult Checkout(string fullName, string address, string phone, string note, string paymentMethod)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey) ?? new List<CartItem>();
            if (cart.Count == 0) return RedirectToAction("Index", "Cart");
            
            // Kiểm tra tồn kho
            foreach (var item in cart)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                {
                    TempData["CheckoutError"] = $"Sản phẩm '{product?.Name ?? "Không xác định"}' không đủ số lượng trong kho.";
                    return RedirectToAction("Index", "Cart");
                }
            }
            
            // Lấy user từ session
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            
            // Tính toán
            decimal subtotal = cart.Sum(item => (item.Product?.DiscountPrice ?? item.Product?.Price ?? 0) * item.Quantity);
            decimal shippingFee = CalculateShippingFee(subtotal);
            string? appliedPromotion = HttpContext.Session.GetString("AppliedPromotion");
            decimal discountAmount = 0;
            
            if (!string.IsNullOrEmpty(appliedPromotion))
            {
                var promotion = _context.Promotions.FirstOrDefault(p => p.Code == appliedPromotion);
                if (promotion != null)
                {
                    if (promotion.DiscountPercent > 0)
                    {
                        discountAmount = subtotal * promotion.DiscountPercent / 100;
                        if (discountAmount > promotion.MaxDiscountAmount)
                            discountAmount = promotion.MaxDiscountAmount;
                    }
                    else
                    {
                        discountAmount = promotion.DiscountAmount;
                    }
                    
                    // Cập nhật số lần sử dụng mã khuyến mãi
                    promotion.UsedCount++;
                    _context.SaveChanges();
                }
            }
            
            decimal total = subtotal + shippingFee - discountAmount;
            
            // Tạo đơn hàng
            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                ShippingAddress = address,
                Phone = phone,
                Note = note,
                PaymentMethod = paymentMethod,
                PromotionCode = appliedPromotion,
                TotalAmount = total,
                Status = "Chờ xác nhận",
                IsPaid = false,
                OrderDetails = cart.Select(i => new OrderDetail
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.Price ?? 0,
                    DiscountUnitPrice = i.Product?.DiscountPrice,
                    TotalLine = (i.Product?.DiscountPrice ?? i.Product?.Price ?? 0) * i.Quantity
                }).ToList()
            };
            
            _context.Orders.Add(order);
            _context.SaveChanges();
            
            // Cập nhật tồn kho
            foreach (var item in cart)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                }
            }
            _context.SaveChanges();
            
            // Xóa giỏ hàng và mã khuyến mãi
            HttpContext.Session.Remove(CartSessionKey);
            HttpContext.Session.Remove("AppliedPromotion");
            
            // Truyền thông tin cho view Confirm
            ViewBag.FullName = fullName;
            ViewBag.Subtotal = subtotal;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.Total = total;
            
            return View("Confirm", order);
        }

        public IActionResult Confirm(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == id);
            
            if (order == null) return NotFound();
            
            return View(order);
        }

        public IActionResult OrderHistory()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login", "Account");
            
            var orders = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == user.Id)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            
            return View(orders);
        }

        public IActionResult OrderDetail(int id)
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login", "Account");
            
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == id && o.UserId == user.Id);
            
            if (order == null) return NotFound();
            
            return View(order);
        }

        [HttpPost]
        public IActionResult ClearOrderHistory()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login", "Account");
            
            var orders = _context.Orders.Where(o => o.UserId == user.Id).ToList();
            _context.Orders.RemoveRange(orders);
            _context.SaveChanges();
            
            TempData["Success"] = "Đã xóa lịch sử đơn hàng.";
            return RedirectToAction("OrderHistory");
        }

        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login", "Account");
            
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id && o.UserId == user.Id);
            
            if (order == null) return NotFound();
            
            if (order.Status == "Chờ xác nhận")
            {
                // Hoàn trả tồn kho
                foreach (var detail in order.OrderDetails)
                {
                    var product = _context.Products.Find(detail.ProductId);
                    if (product != null)
                    {
                        product.Stock += detail.Quantity;
                    }
                }
                
                order.Status = "Đã hủy";
                _context.SaveChanges();
                
                TempData["Success"] = "Đã hủy đơn hàng thành công.";
            }
            else
            {
                TempData["Error"] = "Không thể hủy đơn hàng này.";
            }
            
            return RedirectToAction("OrderHistory");
        }

        private decimal CalculateShippingFee(decimal subtotal)
        {
            if (subtotal >= 2000000) return 0; // Miễn phí vận chuyển cho đơn hàng >= 2 triệu
            if (subtotal >= 1000000) return 30000; // 30k cho đơn hàng >= 1 triệu
            return 50000; // 50k cho đơn hàng < 1 triệu
        }

        [HttpPost]
        public IActionResult ApplyPromotion(string? promotionCode)
        {
            if (string.IsNullOrEmpty(promotionCode))
            {
                TempData["PromotionError"] = "Vui lòng nhập mã khuyến mãi.";
                return RedirectToAction("Checkout");
            }
            
            var promotion = _context.Promotions.FirstOrDefault(p => 
                p.Code == promotionCode && 
                p.IsActive && 
                p.UsedCount < p.MaxUsage &&
                p.EndDate >= DateTime.Now.Date);
            
            if (promotion == null)
            {
                TempData["PromotionError"] = "Mã khuyến mãi không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("Checkout");
            }
            
            HttpContext.Session.SetString("AppliedPromotion", promotionCode);
            TempData["PromotionSuccess"] = $"Đã áp dụng mã khuyến mãi: {promotionCode}";
            
            return RedirectToAction("Checkout");
        }

        public IActionResult RemovePromotion()
        {
            HttpContext.Session.Remove("AppliedPromotion");
            TempData["Success"] = "Đã xóa mã khuyến mãi.";
            return RedirectToAction("Checkout");
        }
    }
} 