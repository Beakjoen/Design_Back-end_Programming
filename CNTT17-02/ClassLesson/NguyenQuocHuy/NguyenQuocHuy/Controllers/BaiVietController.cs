using Microsoft.AspNetCore.Mvc;
using NguyenQuocHuy.Models;
using System.Collections.Generic;
using System;

namespace NguyenQuocHuy.Controllers
{
    public class BaiVietController : Controller
    {
        public static List<BaiViet> BaiViets = new List<BaiViet>
            {
                new BaiViet { MaBaiViet = 1, TieuDe = "Bai Viet 1", NoiDung = "Noi dung bai viet 1", NgayPost = 20231001 },
                new BaiViet { MaBaiViet = 2, TieuDe = "Bai Viet 2", NoiDung = "Noi dung bai viet 2", NgayPost = 20231002 },
                new BaiViet { MaBaiViet = 3, TieuDe = "Bai Viet 3", NoiDung = "Noi dung bai viet 3", NgayPost = 20231003 }
            };

        public IActionResult Index()
        {
            return View(BaiViets);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BaiViet baiViet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    baiViet.MaBaiViet = BaiViets.Count + 1;
                    // Validate NgayPost
                    baiViet.NgayPost = DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day;
                    BaiViets.Add(baiViet);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(baiViet);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
                return View(baiViet);
            }
        }
    }
}
