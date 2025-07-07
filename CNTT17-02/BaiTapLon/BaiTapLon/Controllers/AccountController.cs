using Microsoft.AspNetCore.Mvc;
using BaiTapLon.Models;
using System.Linq;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLon.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserName == userName && u.IsActive);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            // Cập nhật thời gian đăng nhập
            user.LastLoginAt = DateTime.Now;
            _context.SaveChanges();

            HttpContext.Session.SetObjectAsJson("User", user);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string userName, string password, string confirmPassword, string fullName, string email, string phone)
        {
            // Validation
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin bắt buộc (bao gồm email).";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp";
                return View();
            }

            if (password.Length < 6)
            {
                ViewBag.Error = "Mật khẩu phải có ít nhất 6 ký tự";
                return View();
            }

            if (_context.Users.Any(u => u.UserName == userName))
            {
                ViewBag.Error = "Tài khoản đã tồn tại";
                return View();
            }

            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "Email đã được sử dụng";
                return View();
            }

            // Mã hóa mật khẩu
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User { 
                UserName = userName, 
                Password = hashedPassword, 
                FullName = fullName, 
                Email = email,
                Phone = phone,
                IsAdmin = false,
                Address = "",
                IsActive = true,
                CreatedAt = DateTime.Now
            };
            
            _context.Users.Add(user);
            _context.SaveChanges();
            
            HttpContext.Session.SetObjectAsJson("User", user);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login");
        }

        public IActionResult OrderHistory()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login");
            var orders = _context.Orders.Where(o => o.UserId == user.Id).OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }

        public IActionResult OrderLookup(int? orderId)
        {
            Order? order = null;
            if (orderId.HasValue)
            {
                order = _context.Orders.FirstOrDefault(o => o.Id == orderId.Value);
            }
            return View(order);
        }

        public IActionResult Promotion()
        {
            var activePromotions = _context.Promotions
                .Where(p => p.IsActive && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now)
                .OrderBy(p => p.EndDate)
                .ToList();
            return View(activePromotions);
        }

        public IActionResult ViewedProducts()
        {
            var viewedIds = HttpContext.Session.GetObjectFromJson<List<int>>("ViewedProducts") ?? new List<int>();
            var products = _context.Products.Where(p => viewedIds.Contains(p.Id)).ToList();
            return View(products);
        }

        public IActionResult Wishlist()
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return RedirectToAction("Login");
            
            var wishlist = _context.Wishlists
                .Where(w => w.UserId == user.Id)
                .Include(w => w.Product)
                .ThenInclude(p => p.Category)
                .OrderByDescending(w => w.AddedAt)
                .ToList();
                
            return View(wishlist);
        }

        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return Json(new { success = false, message = "Vui lòng đăng nhập" });

            var existingWishlist = _context.Wishlists.FirstOrDefault(w => w.UserId == user.Id && w.ProductId == productId);
            if (existingWishlist != null)
            {
                return Json(new { success = false, message = "Sản phẩm đã có trong danh sách yêu thích" });
            }

            var wishlist = new Wishlist
            {
                UserId = user.Id,
                ProductId = productId,
                AddedAt = DateTime.Now
            };

            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();

            return Json(new { success = true, message = "Đã thêm vào danh sách yêu thích" });
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int wishlistId)
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null) return Json(new { success = false, message = "Vui lòng đăng nhập" });

            var wishlist = _context.Wishlists.FirstOrDefault(w => w.Id == wishlistId && w.UserId == user.Id);
            if (wishlist == null)
            {
                return Json(new { success = false, message = "Không tìm thấy sản phẩm" });
            }

            _context.Wishlists.Remove(wishlist);
            _context.SaveChanges();

            return Json(new { success = true, message = "Đã xóa khỏi danh sách yêu thích" });
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Error = "Vui lòng nhập email.";
                return View();
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy tài khoản với email này.";
                return View();
            }
            // Sinh mã xác nhận 6 số
            var code = new Random().Next(100000, 999999).ToString();
            TempData["ResetCode"] = code;
            TempData["ResetEmail"] = email;
            HttpContext.Session.SetString("ResetCode", code);
            HttpContext.Session.SetString("ResetEmail", email);
            // TODO: Gửi email thực tế, ở đây hiển thị code để test
            TempData["ShowCode"] = code;
            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.Email = HttpContext.Session.GetString("ResetEmail");
            ViewBag.ShowCode = TempData["ShowCode"];
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string code, string newPassword, string confirmPassword)
        {
            var sessionCode = HttpContext.Session.GetString("ResetCode");
            var email = HttpContext.Session.GetString("ResetEmail");
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
                return View();
            }
            if (newPassword != confirmPassword)
            {
                ViewBag.Error = "Mật khẩu xác nhận không khớp.";
                return View();
            }
            if (newPassword.Length < 6)
            {
                ViewBag.Error = "Mật khẩu phải có ít nhất 6 ký tự.";
                return View();
            }
            if (code != sessionCode)
            {
                ViewBag.Error = "Mã xác nhận không đúng hoặc đã hết hạn.";
                return View();
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                ViewBag.Error = "Không tìm thấy tài khoản.";
                return View();
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();
            // Xóa session reset
            HttpContext.Session.Remove("ResetCode");
            HttpContext.Session.Remove("ResetEmail");
            TempData["Success"] = "Đặt lại mật khẩu thành công. Bạn có thể đăng nhập bằng mật khẩu mới.";
            return RedirectToAction("Login");
        }
    }
} 