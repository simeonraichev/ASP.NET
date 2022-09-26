using ASPInstruduction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPInstruduction.Controllers
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
            var model = new TestModel();
            return View();
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Test(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
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