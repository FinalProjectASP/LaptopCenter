using LaptopCenter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LaptopCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
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
            return View();
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
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}