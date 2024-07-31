using LaptopCenter.Data;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using LaptopCenter.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaptopCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private ApplicationDbContext context = new ApplicationDbContext();
        

        public HomeController(IConfiguration configuration, IProductRepository productRepository)
        {
            _configuration = configuration;
            _productRepository = productRepository;

        }

        public async Task<IActionResult> Index()
        {
            return View();
		}

		public async Task<IActionResult> ProductDetail()
		{
			return View();
		}


        public async Task<IActionResult> Product()
        {

            List<Product> productsOnSale = (List<Product>)await _productRepository.GetProducts();
            Console.WriteLine(productsOnSale);
            return View(productsOnSale);
        }

        public async Task<IActionResult> Cart()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            return View();
        }

        public async Task<IActionResult> CheckOut()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            return View();
        }
    }
}