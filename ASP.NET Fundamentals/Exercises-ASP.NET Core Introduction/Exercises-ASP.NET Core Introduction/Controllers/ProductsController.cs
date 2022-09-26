using Exercises_ASP.NET_Core_Introduction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exercises_ASP.NET_Core_Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private IEnumerable<ProductViewModel> products = new List<ProductViewModel>()
        {
            new ProductViewModel()
            {
                Id = 1,
                Name = "Cheese",
                Price = 7.00
            },
            new ProductViewModel()
            {
                Id = 2,
                Name = "Ham",
                Price = 5.50
            },
            new ProductViewModel()
            {
                Id = 3,
                Name = "Bread",
                Price = 1.50
            }
        };
        public IActionResult ById(int id)
        {
            var product = this.products.FirstOrDefault(p=> p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View();
        }
        public IActionResult All()
        {
            return View();
        }
    }
}
