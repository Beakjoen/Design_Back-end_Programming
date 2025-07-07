using Microsoft.AspNetCore.Mvc;
using BaiTapLon.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BaiTapLon.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? search, int? categoryId, string? priceRange, int page = 1, int pageSize = 12, string? sort = null, bool? featured = null, bool? bestSeller = null, 
            string? brand = null, string? productType = null, string? bladeCount = null, string? material = null, string? feature = null, string? rating = null, string? stockStatus = null, bool? hasDiscount = null)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            
            // Lọc theo tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => (p.Name != null && p.Name.Contains(search)) || 
                                             (p.Description != null && p.Description.Contains(search)));
            }
            
            // Lọc theo danh mục
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId);
            }
            
            // Lọc theo thương hiệu
            if (!string.IsNullOrEmpty(brand))
            {
                switch (brand.ToLower())
                {
                    case "panasonic":
                        products = products.Where(p => p.Name != null && p.Name.Contains("Panasonic"));
                        break;
                    case "kdk":
                        products = products.Where(p => p.Name != null && p.Name.Contains("KDK"));
                        break;
                    case "mitsubishi":
                        products = products.Where(p => p.Name != null && p.Name.Contains("Mitsubishi"));
                        break;
                    case "vinawind":
                        products = products.Where(p => p.Name != null && p.Name.Contains("Vinawind"));
                        break;
                    case "kimthuanphong":
                        products = products.Where(p => p.Name != null && p.Name.Contains("Kim Thuận Phong"));
                        break;
                    case "other":
                        products = products.Where(p => p.Name != null && 
                            !p.Name.Contains("Panasonic") && !p.Name.Contains("KDK") && 
                            !p.Name.Contains("Mitsubishi") && !p.Name.Contains("Vinawind") && 
                            !p.Name.Contains("Kim Thuận Phong"));
                        break;
                }
            }
            
            // Lọc theo loại sản phẩm
            if (!string.IsNullOrEmpty(productType))
            {
                switch (productType.ToLower())
                {
                    case "led":
                        products = products.Where(p => p.Name != null && p.Name.Contains("LED"));
                        break;
                    case "chandelier":
                        products = products.Where(p => p.Name != null && p.Name.Contains("chùm"));
                        break;
                    case "usb":
                        products = products.Where(p => p.Name != null && p.Name.Contains("USB"));
                        break;
                    case "opceiling":
                        products = products.Where(p => p.Name != null && p.Name.Contains("ốp trần"));
                        break;
                    case "regular":
                        products = products.Where(p => p.Name != null && 
                            !p.Name.Contains("LED") && !p.Name.Contains("chùm") && 
                            !p.Name.Contains("USB") && !p.Name.Contains("ốp trần"));
                        break;
                }
            }
            
            // Lọc theo số cánh
            if (!string.IsNullOrEmpty(bladeCount))
            {
                switch (bladeCount)
                {
                    case "4":
                        products = products.Where(p => p.Name != null && p.Name.Contains("4 cánh"));
                        break;
                    case "5":
                        products = products.Where(p => p.Name != null && p.Name.Contains("5 cánh"));
                        break;
                }
            }
            
            // Lọc theo chất liệu
            if (!string.IsNullOrEmpty(material))
            {
                switch (material.ToLower())
                {
                    case "wood":
                        products = products.Where(p => p.Name != null && p.Name.Contains("gỗ"));
                        break;
                    case "iron":
                        products = products.Where(p => p.Name != null && p.Name.Contains("sắt"));
                        break;
                    case "plastic":
                        products = products.Where(p => p.Name != null && 
                            !p.Name.Contains("gỗ") && !p.Name.Contains("sắt"));
                        break;
                }
            }
            
            // Lọc theo tính năng
            if (!string.IsNullOrEmpty(feature))
            {
                switch (feature.ToLower())
                {
                    case "remote":
                        products = products.Where(p => p.Name != null && 
                            (p.Name.Contains("điều khiển") || p.Name.Contains("remote")));
                        break;
                    case "led":
                        products = products.Where(p => p.Name != null && p.Name.Contains("LED"));
                        break;
                    case "usb":
                        products = products.Where(p => p.Name != null && p.Name.Contains("USB"));
                        break;
                }
            }
            
            // Lọc theo khoảng giá (cải tiến)
            if (!string.IsNullOrEmpty(priceRange))
            {
                switch (priceRange)
                {
                    case "1": // < 1 triệu
                        products = products.Where(p => (p.DiscountPrice ?? p.Price) < 1000000);
                        break;
                    case "2": // 1-2 triệu
                        products = products.Where(p => (p.DiscountPrice ?? p.Price) >= 1000000 && 
                                                     (p.DiscountPrice ?? p.Price) < 2000000);
                        break;
                    case "3": // 2-3 triệu
                        products = products.Where(p => (p.DiscountPrice ?? p.Price) >= 2000000 && 
                                                     (p.DiscountPrice ?? p.Price) < 3000000);
                        break;
                    case "4": // 3-4 triệu
                        products = products.Where(p => (p.DiscountPrice ?? p.Price) >= 3000000 && 
                                                     (p.DiscountPrice ?? p.Price) < 4000000);
                        break;
                    case "5": // > 4 triệu
                        products = products.Where(p => (p.DiscountPrice ?? p.Price) >= 4000000);
                        break;
                }
            }
            
            // Lọc theo đánh giá
            if (!string.IsNullOrEmpty(rating))
            {
                switch (rating)
                {
                    case "4-5":
                        products = products.Where(p => p.AverageRating >= 4);
                        break;
                    case "3-4":
                        products = products.Where(p => p.AverageRating >= 3 && p.AverageRating < 4);
                        break;
                    case "has-review":
                        products = products.Where(p => p.ReviewCount > 0);
                        break;
                }
            }
            
            // Lọc theo trạng thái hàng
            if (!string.IsNullOrEmpty(stockStatus))
            {
                switch (stockStatus.ToLower())
                {
                    case "instock":
                        products = products.Where(p => p.Stock > 0);
                        break;
                    case "lowstock":
                        products = products.Where(p => p.Stock > 0 && p.Stock <= 5);
                        break;
                    case "outofstock":
                        products = products.Where(p => p.Stock == 0);
                        break;
                }
            }
            
            // Lọc theo sản phẩm nổi bật
            if (featured.HasValue)
            {
                products = products.Where(p => p.IsFeatured == featured.Value);
            }
            
            // Lọc theo sản phẩm bán chạy
            if (bestSeller.HasValue)
            {
                if (bestSeller.Value)
                {
                    products = products.Where(p => p.SoldCount > 0).OrderByDescending(p => p.SoldCount);
                }
            }
            
            // Lọc theo sản phẩm có giảm giá
            if (hasDiscount.HasValue)
            {
                if (hasDiscount.Value)
                {
                    products = products.Where(p => p.DiscountPrice.HasValue && p.DiscountPrice < p.Price);
                }
            }
            
            // Sắp xếp
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "price-asc":
                        products = products.OrderBy(p => p.DiscountPrice ?? p.Price);
                        break;
                    case "price-desc":
                        products = products.OrderByDescending(p => p.DiscountPrice ?? p.Price);
                        break;
                    case "name-asc":
                        products = products.OrderBy(p => p.Name ?? string.Empty);
                        break;
                    case "name-desc":
                        products = products.OrderByDescending(p => p.Name ?? string.Empty);
                        break;
                    case "rating":
                        products = products.OrderByDescending(p => p.AverageRating);
                        break;
                    case "newest":
                        products = products.OrderByDescending(p => p.CreatedAt);
                        break;
                    case "popular":
                        products = products.OrderByDescending(p => p.SoldCount);
                        break;
                    default:
                        products = products.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Name ?? string.Empty);
                        break;
                }
            }
            else
            {
                products = products.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Name ?? string.Empty);
            }
            
            // Phân trang
            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));
            
            var pagedProducts = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            // Truyền dữ liệu cho view
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.PriceRange = priceRange;
            ViewBag.Sort = sort;
            ViewBag.Featured = featured;
            ViewBag.BestSeller = bestSeller;
            ViewBag.Brand = brand;
            ViewBag.ProductType = productType;
            ViewBag.BladeCount = bladeCount;
            ViewBag.Material = material;
            ViewBag.Feature = feature;
            ViewBag.Rating = rating;
            ViewBag.StockStatus = stockStatus;
            ViewBag.HasDiscount = hasDiscount;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalItems = totalItems;
            ViewBag.PageSize = pageSize;
            
            // Lấy danh sách danh mục cho filter
            ViewBag.Categories = _context.Categories.OrderBy(c => c.DisplayOrder).ToList();
            
            return View(pagedProducts);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            // Lấy đánh giá hiển thị
            var reviews = _context.Reviews
                .Where(r => r.ProductId == id && r.IsVisible)
                .Include(r => r.User)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
            ViewBag.Reviews = reviews;

            // Lấy userId nếu đã đăng nhập
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            ViewBag.UserId = user?.Id;

            // Lưu sản phẩm đã xem vào session
            var viewedProducts = HttpContext.Session.GetObjectFromJson<List<int>>("ViewedProducts") ?? new List<int>();
            if (!viewedProducts.Contains(id))
            {
                viewedProducts.Add(id);
                if (viewedProducts.Count > 10) // Giữ tối đa 10 sản phẩm
                {
                    viewedProducts.RemoveAt(0);
                }
                HttpContext.Session.SetObjectAsJson("ViewedProducts", viewedProducts);
            }

            // Lấy sản phẩm liên quan
            var relatedProducts = _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
                .Take(4)
                .ToList();
            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }

        [HttpPost]
        public IActionResult AddReview(int productId, int rating, string? comment)
        {
            var user = HttpContext.Session.GetObjectFromJson<User>("User");
            if (user == null)
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập để đánh giá" });
            }

            if (rating < 1 || rating > 5)
            {
                return Json(new { success = false, message = "Đánh giá phải từ 1-5 sao" });
            }

            // KHÔNG kiểm tra user đã đánh giá sản phẩm này chưa, cho phép nhiều lần

            var review = new Review
            {
                UserId = user.Id,
                ProductId = productId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.Now,
                IsVisible = true
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            // Cập nhật rating trung bình của sản phẩm
            UpdateProductRating(productId);

            return Json(new { success = true, message = "Đánh giá đã được gửi thành công" });
        }

        private void UpdateProductRating(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                var reviews = _context.Reviews.Where(r => r.ProductId == productId && r.IsVisible).ToList();
                if (reviews.Any())
                {
                    product.AverageRating = reviews.Average(r => r.Rating);
                    product.ReviewCount = reviews.Count;
                }
                else
                {
                    product.AverageRating = 0;
                    product.ReviewCount = 0;
                }
                _context.SaveChanges();
            }
        }

        public IActionResult Featured()
        {
            var featuredProducts = _context.Products
                .Where(p => p.IsFeatured)
                .Include(p => p.Category)
                .OrderBy(p => p.DisplayOrder)
                .ToList();
            
            return View(featuredProducts);
        }

        public IActionResult BestSellers()
        {
            var bestSellers = _context.Products
                .Where(p => p.SoldCount > 0)
                .Include(p => p.Category)
                .OrderByDescending(p => p.SoldCount)
                .Take(12)
                .ToList();
            
            return View(bestSellers);
        }

        public IActionResult Category(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            
            var products = _context.Products
                .Where(p => p.CategoryId == id)
                .Include(p => p.Category)
                .OrderBy(p => p.DisplayOrder)
                .ToList();
            
            ViewBag.Category = category;
            return View(products);
        }

#if DEBUG
[HttpPost]
public IActionResult AdminQuickSale40()
{
    var products = _context.Products.OrderBy(p => p.Id).Take(5).ToList();
    foreach (var p in products)
    {
        p.SalePercent = 40;
        // Nếu muốn cập nhật giá giảm, có thể thêm dòng sau:
        // p.DiscountPrice = p.Price * 0.6m;
    }
    _context.SaveChanges();
    return Content("Đã cập nhật 5 sản phẩm đầu tiên thành Sale 40%!");
}
#endif
    }
} 