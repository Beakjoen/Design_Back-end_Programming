using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService = new StudentService();
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();
            return View(students);
        }
    }
}
