using HocDBFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace HOCDBFirst.Controllers
{
    public class PhongBanController : Controller
    {
        private readonly QlnhanSuContext _context;
        public PhongBanController(QlnhanSuContext context)

        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.PhongBans.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PhongBan phongBan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.PhongBans.Add(phongBan);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(phongBan);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi thêm phòng ban: " + ex.Message);
                return View(phongBan);
            }
        }

        public IActionResult Edit(string id)
        {
            return View(_context.PhongBans.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(PhongBan phongBan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.PhongBans.Update(phongBan);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(phongBan);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật phòng ban: " + ex.Message);
                return View(phongBan);
            }
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var phongBan = _context.PhongBans.Find(id);
            _context.PhongBans.Remove(phongBan);
            _context.SaveChanges();
            return View(phongBan);
        }
    }
}