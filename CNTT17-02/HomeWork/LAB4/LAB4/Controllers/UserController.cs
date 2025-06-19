using Microsoft.AspNetCore.Mvc;
using LAB4.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LAB4.Controllers
{
    public class UserController : Controller
    {
        // Static list để lưu trữ users (trong thực tế sẽ dùng database)
        private static List<User> users = new List<User>
        {
            new User { id = "SV001", username = "admin", password = "123456", email = "admin@gmail.com", phone = "0123456789" },
            new User { id = "SV002", username = "user1", password = "123456", email = "user1@gmail.com", phone = "0987654321" }
        };

        // GET: User/Index - Hiển thị danh sách users
        public IActionResult Index()
        {
            return View(users);
        }

        // GET: User/Add - Hiển thị form thêm user
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: User/Add - Xử lý thêm user
        [HttpPost]
        public IActionResult Add(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra id đã tồn tại chưa
                    if (users.Any(u => u.id == user.id))
                    {
                        ModelState.AddModelError("id", "Mã sinh viên đã tồn tại!");
                        return View(user);
                    }

                    // Kiểm tra username đã tồn tại chưa
                    if (users.Any(u => u.username == user.username))
                    {
                        ModelState.AddModelError("username", "Tài khoản đã tồn tại!");
                        return View(user);
                    }

                    users.Add(user);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(user);
        }

        // GET: User/Edit/id - Hiển thị form sửa user
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Edit - Xử lý sửa user
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = users.FirstOrDefault(u => u.id == user.id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Kiểm tra username đã tồn tại chưa (trừ user hiện tại)
                    if (users.Any(u => u.username == user.username && u.id != user.id))
                    {
                        ModelState.AddModelError("username", "Tài khoản đã tồn tại!");
                        return View(user);
                    }

                    // Cập nhật thông tin
                    existingUser.username = user.username;
                    existingUser.password = user.password;
                    existingUser.email = user.email;
                    existingUser.phone = user.phone;

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(user);
        }

        // GET: User/Delete/id - Xóa user
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = users.FirstOrDefault(u => u.id == id);
                if (user != null)
                {
                    users.Remove(user);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: User/Details/id - Xem chi tiết user
        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = users.FirstOrDefault(u => u.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
