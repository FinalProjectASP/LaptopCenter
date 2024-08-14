using LaptopCenter.Constraints;
using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories;
using LaptopCenter.Repositories.Interfaces;
using LaptopCenter.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

namespace LaptopCenter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private ApplicationDbContext _context;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;
        

        public HomeController(
            ApplicationDbContext context, 
            IConfiguration configuration, 
            IProductRepository productRepository, 
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepository
        )
        {
            _cartRepository = cartRepository;
            _context = context;
            _configuration = configuration;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;

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

        public IActionResult Cart()
        {
            // Assuming you have a way to get the current user's ID
            string userId = "trannq"; // Example
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            string userId = "trannq"; // Example
            var cartItem = _cartRepository.GetCartByUserAndProduct(userId, productId);
            if (cartItem != null)
            {   
                _cartRepository.RemoveCartItem(cartItem);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Cart item not found." });
        }

        [HttpPost]
        public IActionResult AddToCart( int ProductId, int Quantity)
        {
            string UserId = "trannq";
            var existingCart = _cartRepository.GetCartByUserAndProduct(UserId, ProductId);

            if (existingCart != null)
            {
                // Update the quantity if the product is already in the cart
                existingCart.Quantity += Quantity;
                _cartRepository.UpdateCart(existingCart);
            }
            else
            {
                // Add a new cart item if it's not already in the cart
                var cart = new Cart
                {
                    UserId = UserId,
                    ProductId = ProductId,
                    Quantity = Quantity,
                    CreatedDate = DateTime.Now
                };
                _cartRepository.AddToCart(cart);
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(int productId, int quantity)
        {
            string userId = "trannq"; // Example
            var cartItem = _cartRepository.GetCartByUserAndProduct(userId, productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _cartRepository.UpdateCart(cartItem);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Cart item not found." });
        }

        public async Task<IActionResult> Profile()
        {
            string userId = "trannq"; // Example
            List<OrderDTO> getHistory = _orderRepository.GetHistoryPurchase(userId);
            List<OrderDTO> getOrder = _orderRepository.GetCurrentOrder(userId);

            ViewBag.History = getHistory;
            ViewBag.Ordered = getOrder;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            string userId = "trannq"; // Example
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCheckout(string customerName, string customerPhone, string address, decimal total)
        {
            // Get the current user's ID
            var userId = "trannq"; // Example

            // Retrieve cart items for the user
            List<Cart> cartItems = _cartRepository.GetCartItemsByUserId(userId);


            if (cartItems.Any())
            {
                // Create a new order
                var order = new Order
                {
                    UserId = userId,
                    CustomerName = customerName,
                    CustomerPhone = customerPhone,
                    DeleveryLocal = address,
                    OrderDate = DateTime.Now,
                    DeliveryDate = DateTime.Now.AddDays(5), // Example delivery date
                    Total = total,
                    Status = EStatus.Pending, // Assuming EStatus is an enum
                    OrderDetails = cartItems.Select(c => new OrderDetail
                    {
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        UnitPrice = c.Product.Price
                    }).ToList()
                };

                // Save the order to the database
                _orderRepository.AddOrder(order);

                // Clear the user's cart
                _cartRepository.ClearCart(userId);

                // Redirect to a confirmation page (or display success message)
                return RedirectToAction("Cart");
            }

            // If the cart is empty, show an error message
            ViewBag.ErrorMessage = "Your cart is empty!";
            return View("CheckOut");
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