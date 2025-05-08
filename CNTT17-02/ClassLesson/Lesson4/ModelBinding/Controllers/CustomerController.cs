using Microsoft.AspNetCore.Mvc;
using ModelBinding.Models;

namespace ModelBinding.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult FromRoute()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FromRoute([FromRoute] string Id, Customer customer)
        {
            return Content($"Id in Route: {Id},Id in Form: {customer.Id}");
        }

        [HttpGet]
        public IActionResult FormAndQuery([FromQuery] string name, Customer customer)
        {
            return Content($"Name in Query: {name}, Name in Form: {customer.Name}");
        }

    }
}