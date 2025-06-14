using HocValkidations.Models;
using Microsoft.AspNetCore.Mvc;

namespace HocValkidations.Controllers
{
    public class NhanVienController : Controller
    {
        public static List<NhanVien> nhanVienList = new List<NhanVien>()
                {
                    new() { HoTen = "Nguyen Van An", Tuoi = 33, Email = "An123@gmail.com" },
                    new() { HoTen = "Tran Thi Bich", Tuoi = 28, Email = "TTB@gmail.com" },
                    new() { HoTen = "Le Van Long", Tuoi = 35, Email = "Long66@gmail.com" }
                };

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nhanVienList.Add(new NhanVien()
                    {
                        HoTen = nhanVien.HoTen,
                        Tuoi = nhanVien.Tuoi,
                        Email = nhanVien.Email
                    });
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.ToString());
            }
            return View(nhanVien);
        }
    }
}
