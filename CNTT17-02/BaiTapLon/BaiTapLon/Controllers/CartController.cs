using Microsoft.AspNetCore.Mvc;
using BaiTapLon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLon.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private const string CartSessionKey = "CartSession";
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // Lấy giỏ hàng từ Session
        private List<CartItem> GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartSessionKey);
            if (cart == null)
            {
                cart = new List<CartItem>();
                HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
            }
            return cart;
        }

        // Lưu giỏ hàng vào Session
        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson(CartSessionKey, cart);
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            
            // Cập nhật thông tin sản phẩm từ database
            foreach (var item in cart)
            {
                var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    item.Product = product;
                    // Kiểm tra tồn kho
                    if (item.Quantity > product.Stock)
                    {
                        item.Quantity = product.Stock;
                        TempData["StockWarning"] = $"Sản phẩm '{product.Name}' chỉ còn {product.Stock} cái trong kho.";
                    }
                }
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
                }
            }
            
            decimal total = subtotal + shippingFee - discountAmount;
            
            ViewBag.Subtotal = subtotal;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.Total = total;
            ViewBag.AppliedPromotion = appliedPromotion;
            ViewBag.TotalItems = cart.Sum(item => item.Quantity);
            
            return View(cart);
        }

        [HttpGet]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            return AddToCartInternal(productId, quantity);
        }

        [HttpPost]
        [ActionName("AddToCart")]
        public IActionResult AddToCartPost(int productId, int quantity = 1)
        {
            return AddToCartInternal(productId, quantity);
        }

        // Logic chung cho cả GET và POST
        private IActionResult AddToCartInternal(int productId, int quantity = 1)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null) return NotFound();
            
            if (product.Status == "Ngừng kinh doanh")
            {
                TempData["AddToCartError"] = "Sản phẩm này đã ngừng kinh doanh.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            
            if (product.Stock <= 0)
            {
                TempData["AddToCartError"] = "Sản phẩm này đã hết hàng.";
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            
            if (item == null)
            {
                cart.Add(new CartItem { 
                    ProductId = productId, 
                    Product = product, 
                    Quantity = System.Math.Min(quantity, product.Stock) 
                });
            }
            else
            {
                var newQuantity = item.Quantity + quantity;
                item.Quantity = System.Math.Min(newQuantity, product.Stock);
            }
            
            SaveCart(cart);
            TempData["AddToCartSuccess"] = $"Đã thêm '{product.Name}' vào giỏ hàng!";
            return RedirectToAction("Details", "Product", new { id = productId });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            
            if (item != null)
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    if (quantity <= 0)
                    {
                        cart.Remove(item);
                    }
                    else if (quantity <= product.Stock)
                    {
                        item.Quantity = quantity;
                    }
                    else
                    {
                        item.Quantity = product.Stock;
                        TempData["StockWarning"] = $"Sản phẩm '{product.Name}' chỉ còn {product.Stock} cái trong kho.";
                    }
                }
                SaveCart(cart);
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
                TempData["RemoveFromCartSuccess"] = "Đã xóa sản phẩm khỏi giỏ hàng!";
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            TempData["ClearCartSuccess"] = "Đã xóa toàn bộ giỏ hàng!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ApplyPromotion(string? promotionCode)
        {
            if (string.IsNullOrEmpty(promotionCode))
            {
                TempData["PromotionError"] = "Vui lòng nhập mã khuyến mãi.";
                return RedirectToAction("Index");
            }
            
            var promotion = _context.Promotions.FirstOrDefault(p => 
                p.Code == promotionCode && 
                p.IsActive && 
                p.UsedCount < p.MaxUsage &&
                p.ExpiryDate > DateTime.Now);
            
            if (promotion == null)
            {
                TempData["PromotionError"] = "Mã khuyến mãi không hợp lệ hoặc đã hết hạn.";
                return RedirectToAction("Index");
            }
            
            var cart = GetCart();
            decimal subtotal = cart.Sum(item => (item.Product?.DiscountPrice ?? item.Product?.Price ?? 0) * item.Quantity);
            
            if (subtotal < promotion.MinOrderAmount)
            {
                TempData["PromotionError"] = $"Đơn hàng tối thiểu {promotion.MinOrderAmount:N0} VNĐ để áp dụng mã này.";
                return RedirectToAction("Index");
            }
            
            HttpContext.Session.SetString("AppliedPromotion", promotionCode);
            TempData["PromotionSuccess"] = $"Đã áp dụng mã khuyến mãi '{promotion.Name}'!";
            
            return RedirectToAction("Index");
        }

        public IActionResult RemovePromotion()
        {
            HttpContext.Session.Remove("AppliedPromotion");
            TempData["PromotionRemoved"] = "Đã xóa mã khuyến mãi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCart();
            return Json(new { count = cart.Sum(item => item.Quantity) });
        }

        private decimal CalculateShippingFee(decimal subtotal)
        {
            if (subtotal >= 2000000) return 0; // Miễn phí vận chuyển cho đơn hàng >= 2 triệu
            if (subtotal >= 1000000) return 30000; // 30k cho đơn hàng >= 1 triệu
            return 50000; // 50k cho đơn hàng < 1 triệu
        }
    }
} 