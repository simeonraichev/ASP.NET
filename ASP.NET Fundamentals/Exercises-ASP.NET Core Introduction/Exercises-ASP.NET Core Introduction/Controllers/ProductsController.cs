using Exercises_ASP.NET_Core_Introduction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
            var product = this.products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }
        public IActionResult All()
        {
            return View(this.products);
        }
        public IActionResult AllAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            return Json(products, options);
        }
        public IActionResult AllAsText()
        {
            var text = string.Empty;
            foreach (var pr in products)
            {
                text += $"Products {pr.Id}: {pr.Name} - {pr.Price}lv";
                text += "\r\n";
            }
            return Content(text);
        }
    }
}
