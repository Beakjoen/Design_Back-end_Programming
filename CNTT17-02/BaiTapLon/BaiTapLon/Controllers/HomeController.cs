using System.Diagnostics;
using BaiTapLon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BaiTapLon.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Sản phẩm giảm 40%
            var sale40Products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.SalePercent == 40 && p.Status != "Ngừng kinh doanh")
                .OrderByDescending(p => p.CreatedAt)
                .Take(8)
                .ToList();

            // Sản phẩm thường (không sale 40%)
            var normalProducts = _context.Products
                .Include(p => p.Category)
                .Where(p => (p.SalePercent == null || p.SalePercent == 0) && p.Status != "Ngừng kinh doanh")
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            ViewBag.Sale40Products = sale40Products;
            return View(normalProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Search(string? q)
        {
            if (string.IsNullOrEmpty(q))
                return RedirectToAction("Index");
            
            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => (p.Name != null && p.Name.Contains(q)) || 
                           (p.Description != null && p.Description.Contains(q)) ||
                           (p.Category != null && p.Category.Name != null && p.Category.Name.Contains(q)))
                .Where(p => p.Status != "Ngừng kinh doanh")
                .OrderByDescending(p => p.CreatedAt)
                .ToList();
            
            ViewBag.SearchTerm = q;
            ViewBag.SearchResults = products.Count;
            
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
