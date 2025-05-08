using System.Diagnostics;
using HocDataTransfer.Models;
using Microsoft.AspNetCore.Mvc;

namespace HocDataTransfer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Cac cach truyen du lieu tu controller sang view
            //Cach 1 : su dung ViewBag
            ViewBag.Text = "ViewBag la 1 dynamic de truyen du lieu tu controller sang view cung 1 request";
            //Cach 2 : su dung ViewData
            ViewData["Text"] = "ViewData la 1 dictionary de truyen du lieu tu controller sang view cung 1 request" +
                "Ở bên View phải ép kiểu tường sinh";
            List<string> names = new List<string>()
            {
                "Ba","Ba","Boy"
            };
            ViewData["Names"] = names;
            //Cach 3 : su dung TempData
            TempData["Text"] = "TempData la 1 dictionary de truyen du lieu tu controller sang view khac request";
            //return View();
            return RedirectToAction("GetData");
        }

        public IActionResult GetData()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
