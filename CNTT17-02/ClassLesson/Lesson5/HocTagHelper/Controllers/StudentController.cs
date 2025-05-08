using Microsoft.AspNetCore.Mvc;

namespace HocTagHelper.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            return View();
        }
    }
}
