using Microsoft.AspNetCore.Mvc;
using ReceivingParameter.Models;

namespace ReceivingParameter.Controllers
{
    public class StudentController : Controller
    {
        //khởi tạo 1 danh sách sinh viên
        private static List<Student> _students = new List<Student>
        {
            new Student { id = "1", name = "Nguyen Van A", address = "Ha Noi" },
            new Student { id = "2", name = "Nguyen Van B", address = "Da Nang" },
            new Student { id = "3", name = "Nguyen Van C", address = "Ho Chi Minh" }
        };
        public IActionResult Index()
        {
            //trả về danh sách sinh viên
            return View(_students);
        }
        public IActionResult Create()
        {
            //trả về danh sách sinh viên
            return View();
        }
        public IActionResult Save()
        {
            Student student = new Student();
            //lấy dữ liệu từ form
            student.id = Request.Form["id"];
            student.name = Request.Form["name"];
            student.address = Request.Form["address"];
            //thêm sinh viên vào danh sách
            _students.Add(student);
            //trả về danh sách sinh viên
            return RedirectToAction("Index");
        }
    }
}
