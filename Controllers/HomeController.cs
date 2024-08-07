using LaptopCenter.Data;
using LaptopCenter.DTO;
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
            List<ProductsDTO> getLatestProducts = (List<ProductsDTO>)await _productRepository.GetLatestProducts();
            List<ProductsDTO> topSellingProducts = (List<ProductsDTO>)await _productRepository.GetTopSellingProducts();
            
            ViewBag.LatestProducts = getLatestProducts;
            ViewBag.TopSellingProducts = topSellingProducts;

            return View();
		}

		public async Task<IActionResult> ProductDetail(int productId)
		{
            ProductsDTO getOneProduct = await _productRepository.GetOneProduct(productId);
            List<ProductsDTO> getSuggestProduct = (List<ProductsDTO>)await _productRepository.GetSuggestProducts(getOneProduct.Supplier.SupplierName);

            ViewBag.Product = getOneProduct;
            ViewBag.SuggestProduct = getSuggestProduct;
            return View();
		}


        public async Task<IActionResult> Product()
        {

            List<ProductsDTO> productsOnSale = (List<ProductsDTO>)await _productRepository.GetProducts("getOnSale");
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