using Microsoft.AspNetCore.Mvc;
using BaiTapLon.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

namespace BaiTapLon.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            return user != null && user.IsAdmin;
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            // Thống kê tổng quan
            var totalProducts = _context.Products.Count();
            var totalOrders = _context.Orders.Count();
            var totalUsers = _context.Users.Count();
            // Doanh thu chỉ tính đơn Hoàn thành hoặc Đã giao hàng
            var totalRevenue = _context.Orders.Where(o => o.Status == "Hoàn thành" || o.Status == "Đã giao hàng").Sum(o => o.TotalAmount);
            // Doanh thu tháng này
            var now = DateTime.Now;
            var monthRevenue = _context.Orders.Where(o => (o.Status == "Hoàn thành" || o.Status == "Đã giao hàng") && o.OrderDate.Month == now.Month && o.OrderDate.Year == now.Year).Sum(o => o.TotalAmount);
            
            // Đơn hàng gần đây
            var recentOrders = _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToList();
            
            // Sản phẩm bán chạy
            var bestSellingProducts = _context.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => od.ProductId)
                .Select(g => new
                {
                    Product = g.First().Product,
                    TotalSold = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .ToList();
            
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.MonthRevenue = monthRevenue;
            ViewBag.RecentOrders = recentOrders;
            ViewBag.BestSellingProducts = bestSellingProducts;
            
            return View();
        }

        public IActionResult ProductManager()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var products = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            
            return View(products);
        }

        [HttpPost]
        public IActionResult UpdateProductStatus(int productId, string status)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Status = status;
                product.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                TempData["Success"] = "Đã cập nhật trạng thái sản phẩm!";
            }
            
            return RedirectToAction("ProductManager");
        }

        [HttpPost]
        public IActionResult UpdateProductFeature(int productId, bool isFeatured)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.IsFeatured = isFeatured;
                product.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                TempData["Success"] = "Đã cập nhật sản phẩm nổi bật!";
            }
            
            return RedirectToAction("ProductManager");
        }

        public IActionResult OrderManager()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
            
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int orderId, string status)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.SaveChanges();
                TempData["Success"] = "Đã cập nhật trạng thái đơn hàng!";
            }
            
            return RedirectToAction("OrderManager");
        }

        public IActionResult OrderDetail(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var order = _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == id);
            
            if (order == null) return NotFound();
            
            return View(order);
        }

        public IActionResult UserManager()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var users = _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToList();
            
            return View(users);
        }

        [HttpPost]
        public IActionResult UpdateUserStatus(int userId, bool isActive)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.IsActive = isActive;
                _context.SaveChanges();
                TempData["Success"] = "Đã cập nhật trạng thái người dùng!";
            }
            
            return RedirectToAction("UserManager");
        }

        public IActionResult PromotionManager()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var promotions = _context.Promotions
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            
            return View(promotions);
        }

        [HttpPost]
        public IActionResult CreatePromotion(Promotion promotion)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            if (ModelState.IsValid)
            {
                promotion.CreatedAt = DateTime.Now;
                promotion.IsActive = true;
                promotion.UsedCount = 0;
                
                _context.Promotions.Add(promotion);
                _context.SaveChanges();
                TempData["Success"] = "Đã tạo mã khuyến mãi mới!";
            }
            
            return RedirectToAction("PromotionManager");
        }

        [HttpPost]
        public IActionResult UpdatePromotionStatus(int promotionId, bool isActive)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var promotion = _context.Promotions.Find(promotionId);
            if (promotion != null)
            {
                promotion.IsActive = isActive;
                _context.SaveChanges();
                TempData["Success"] = "Đã cập nhật trạng thái khuyến mãi!";
            }
            
            return RedirectToAction("PromotionManager");
        }

        [HttpPost]
        public IActionResult DeletePromotion(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var promo = _context.Promotions.Find(id);
            if (promo != null)
            {
                _context.Promotions.Remove(promo);
                _context.SaveChanges();
                TempData["Success"] = "Đã xóa mã khuyến mãi!";
            }
            return RedirectToAction("PromotionManager");
        }

        public IActionResult ReviewManager()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var reviews = _context.Reviews
                .Include(r => r.Product)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            
            return View(reviews);
        }

        [HttpPost]
        public IActionResult UpdateReviewVisibility(int reviewId, bool isVisible)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            var review = _context.Reviews.Find(reviewId);
            if (review != null)
            {
                review.IsVisible = isVisible;
                _context.SaveChanges();
                
                // Cập nhật lại đánh giá trung bình cho sản phẩm
                var productController = new ProductController(_context);
                var updateMethod = typeof(ProductController).GetMethod("UpdateProductRating", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                updateMethod?.Invoke(productController, new object[] { review.ProductId });
                
                TempData["Success"] = "Đã cập nhật trạng thái đánh giá!";
            }
            
            return RedirectToAction("ReviewManager");
        }

        public IActionResult Statistics()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            // Tổng doanh thu
            var totalRevenue = _context.Orders.Where(o => o.Status == "Hoàn thành" || o.Status == "Đã giao hàng").Sum(o => o.TotalAmount);
            // Doanh thu tháng này
            var now = DateTime.Now;
            var monthRevenue = _context.Orders.Where(o => (o.Status == "Hoàn thành" || o.Status == "Đã giao hàng") && o.OrderDate.Month == now.Month && o.OrderDate.Year == now.Year).Sum(o => o.TotalAmount);
            // Đơn hoàn thành
            var completedOrders = _context.Orders.Count(o => o.Status == "Hoàn thành" || o.Status == "Đã giao hàng");
            // Đơn đang xử lý
            var processingOrders = _context.Orders.Count(o => o.Status == "Đang xử lý" || o.Status == "Đang giao hàng");
            // Đơn bị hủy
            var canceledOrders = _context.Orders.Count(o => o.Status == "Đã hủy");
            // Doanh thu theo tháng (key: yyyyMM, value: sum)
            var monthlyRevenue = _context.Orders
                .Where(o => o.Status == "Hoàn thành" || o.Status == "Đã giao hàng")
                .GroupBy(o => o.OrderDate.Year * 100 + o.OrderDate.Month)
                .ToDictionary(g => g.Key, g => g.Sum(x => x.TotalAmount));
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.MonthRevenue = monthRevenue;
            ViewBag.CompletedOrders = completedOrders;
            ViewBag.ProcessingOrders = processingOrders;
            ViewBag.CanceledOrders = canceledOrders;
            ViewBag.MonthlyRevenue = monthlyRevenue;
            return View();
        }

        [HttpPost]
        public IActionResult AdminQuickSale40()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var products = _context.Products.Where(p => p.Status != "Ngừng kinh doanh").OrderBy(p => p.Id).Take(5).ToList();
            foreach (var p in products)
            {
                p.SalePercent = 40;
                p.UpdatedAt = DateTime.Now;
            }
            _context.SaveChanges();
            TempData["Success"] = $"Đã gán SalePercent = 40 cho {products.Count} sản phẩm đầu tiên.";
            return RedirectToAction("ProductManager");
        }

        [HttpGet]
        public IActionResult AdminSaleAll()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var ids = new int[] {1,2,3,4,5,6,7,8};
            var products = _context.Products.Where(p => ids.Contains(p.Id)).ToList();
            foreach (var p in products)
            {
                p.SalePercent = 40;
                p.Status = "Còn hàng";
                p.UpdatedAt = DateTime.Now;
            }
            _context.SaveChanges();
            TempData["Success"] = $"Đã gán SalePercent = 40 cho {products.Count} sản phẩm (Id 1-8).";
            return RedirectToAction("ProductManager");
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            ViewBag.Categories = _context.Categories.OrderBy(c => c.DisplayOrder).ToList();
            // Lấy tất cả ảnh từ wwwroot/image và các thư mục con
            var imageRoot = Path.Combine(Directory.GetCurrentDirectory(), "BaiTapLon", "wwwroot", "image");
            var images = new List<string>();
            if (Directory.Exists(imageRoot))
            {
                var files = Directory.GetFiles(imageRoot, "*.*", SearchOption.AllDirectories)
                    .Where(f => new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(f).ToLower()));
                foreach (var f in files)
                {
                    var rel = f.Replace(imageRoot, "").Replace("\\", "/").TrimStart('/');
                    images.Add("/image/" + rel);
                }
            }
            ViewBag.ImageFiles = images;
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var imageUrl = Request.Form["ImageUrl"].ToString();
            var file = Request.Form.Files["ImageFile"];
            bool uploadSuccess = false;
            string uploadedFileUrl = null;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                product.ImageUrl = imageUrl;
                uploadSuccess = true;
            }
            else if (file != null && file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName).ToLower();
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowed.Contains(ext))
                {
                    ModelState.AddModelError("ImageFile", "Chỉ cho phép file ảnh jpg, png, gif.");
                }
                else
                {
                    try
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName) + ext;
                        var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "BaiTapLon", "wwwroot", "image", "sale");
                        if (!Directory.Exists(imageDir))
                        {
                            Directory.CreateDirectory(imageDir);
                        }
                        var savePath = Path.Combine(imageDir, fileName);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        product.ImageUrl = "/image/sale/" + fileName;
                        uploadedFileUrl = product.ImageUrl;
                        uploadSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ImageFile", "Lỗi khi lưu file ảnh: " + ex.Message);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Vui lòng chọn ảnh đại diện.");
            }
            if (ModelState.IsValid && uploadSuccess)
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["Success"] = "Đã thêm sản phẩm mới thành công!";
                return RedirectToAction("ProductManager");
            }
            ViewBag.Categories = _context.Categories.OrderBy(c => c.DisplayOrder).ToList();
            // Lấy lại gallery ảnh (bao gồm file vừa upload nếu thành công)
            var imageRoot = Path.Combine(Directory.GetCurrentDirectory(), "BaiTapLon", "wwwroot", "image");
            var images = new List<string>();
            if (Directory.Exists(imageRoot))
            {
                var files = Directory.GetFiles(imageRoot, "*.*", SearchOption.AllDirectories)
                    .Where(f => new[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(f).ToLower()));
                foreach (var f in files)
                {
                    var rel = f.Replace(imageRoot, "").Replace("\\", "/").TrimStart('/');
                    images.Add("/image/" + rel);
                }
            }
            // Ưu tiên highlight ảnh vừa upload nếu có
            if (!string.IsNullOrEmpty(uploadedFileUrl) && !images.Contains(uploadedFileUrl))
                images.Insert(0, uploadedFileUrl);
            ViewBag.ImageFiles = images;
            return View(product);
        }

        [HttpPost]
        public IActionResult UploadImage()
        {
            if (!IsAdmin()) return Json(new { success = false, message = "Bạn không có quyền." });
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "Chưa chọn file." });
            var ext = Path.GetExtension(file.FileName).ToLower();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowed.Contains(ext))
                return Json(new { success = false, message = "Chỉ cho phép file ảnh jpg, png, gif." });
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + ext;
            var savePath = Path.Combine(Directory.GetCurrentDirectory(), "BaiTapLon", "wwwroot", "image", fileName);
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Json(new { success = true, url = "/image/" + fileName });
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Success"] = "Đã xóa sản phẩm thành công!";
            }
            else
            {
                TempData["Error"] = "Không tìm thấy sản phẩm để xóa!";
            }
            return RedirectToAction("ProductManager");
        }

        [HttpPost]
        public IActionResult AdminResetPassword(int userId)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy người dùng.";
                return RedirectToAction("UserManager");
            }
            // Sinh mật khẩu ngẫu nhiên 8 ký tự
            var chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var random = new Random();
            var newPassword = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();
            TempData["ResetPasswordInfo"] = $"Đã đặt lại mật khẩu cho tài khoản {user.UserName}. Mật khẩu mới: <b>{newPassword}</b>";
            return RedirectToAction("UserManager");
        }
    }
} 