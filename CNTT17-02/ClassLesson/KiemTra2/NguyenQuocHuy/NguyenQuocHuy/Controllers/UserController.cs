using Microsoft.AspNetCore.Mvc;
using NguyenQuocHuy.Models;
using System.Linq;

namespace NguyenQuocHuy.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                ModelState.AddModelError("", "Lỗi khi thêm người dùng: " + ex.Message);
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật người dùng: " + ex.Message);
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null) return NotFound();

                _context.Users.Remove(user);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi xóa người dùng: " + ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
