using LaptopCenter.Constraints;
using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories;
using LaptopCenter.Repositories.Interfaces;
using LaptopCenter.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace LaptopCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;


        public HomeController(
            UserManager<AppUser> userManager,
            IProductRepository productRepository,
            IReviewRepository reviewRepository
        )
        {
            _userManager = userManager;
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<IActionResult> Index()
        {
            AppUser appUser = await _userManager.GetUserAsync(User);
            if (appUser != null)
            {
                var role = await _userManager.GetRolesAsync(appUser);

                if (role[0] == ERole.Admin.ToString())
                {
                    return LocalRedirect(Url.Content("~/Admin/Orders/Index"));
                }
            }

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
            List<Review> getReview = _reviewRepository.GetReviewsByProductId(productId);

            // Calculate the average rating
            decimal averageRating = (decimal)(getReview.Count > 0 ? getReview.Average(r => r.Rate) : 0);

            ViewBag.Product = getOneProduct;
            ViewBag.Review = getReview;
            ViewBag.SuggestProduct = getSuggestProduct;
            ViewBag.AverageRating = averageRating;

            return View();
        }

        public async Task<IActionResult> Product()
        {
            List<ProductsDTO> productsOnSale = (List<ProductsDTO>)await _productRepository.GetProducts("getOnSale");
            return View(productsOnSale);
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