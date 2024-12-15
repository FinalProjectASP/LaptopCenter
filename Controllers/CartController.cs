using LaptopCenter.Constraints;
using LaptopCenter.Models;
using Microsoft.AspNetCore.Mvc;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using LaptopCenter.DTO;
using System.Net;

namespace LaptopCenter.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;


        public CartController(
            UserManager<AppUser> userManager,
            IProductRepository productRepository,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IReviewRepository reviewRepository
        )
        {
            _userManager = userManager;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;

        }

        public IActionResult Index()
        {
            // Assuming you have a way to get the current user's ID
            string userId = _userManager.GetUserId(User);
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            string userId = _userManager.GetUserId(User);
            var cartItem = _cartRepository.GetCartByUserAndProduct(userId, productId);
            if (cartItem != null)
            {
                _cartRepository.RemoveCartItem(cartItem);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Cart item not found." });
        }

        [HttpPost]
        public IActionResult AddToCart(int ProductId, int Quantity)
        {
            string UserId = _userManager.GetUserId(User);
            var existingCart = _cartRepository.GetCartByUserAndProduct(UserId, ProductId);

            if (existingCart != null)
            {
                if (existingCart.Quantity + 1 < existingCart.Product.Quantity - existingCart.Product.SoldQuantity)
                {
                    // Update the quantity if the product is already in the cart
                    existingCart.Quantity += Quantity;
                    _cartRepository.UpdateCart(existingCart);
                }
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCartQuantity(int productId, int quantity)
        {
            string userId = _userManager.GetUserId(User);
            var cartItem = _cartRepository.GetCartByUserAndProduct(userId, productId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                _cartRepository.UpdateCart(cartItem);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Cart item not found." });
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            string userId = _userManager.GetUserId(User);
            var cartItems = _cartRepository.GetCartItemsByUserId(userId);

            CheckoutDTO checkoutDTO = new CheckoutDTO
            {
                CartItems = cartItems
            };

            return View(checkoutDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckoutDTO checkoutDTO)
        {
            // Get the current user's ID
            var userId = _userManager.GetUserId(User);

            // Retrieve cart items for the user
            List<Cart> cartItems = _cartRepository.GetCartItemsByUserId(userId);

            if (ModelState.IsValid)
            {
                if (cartItems.Any())
                {
                    // Create a new order
                    var order = new Order
                    {
                        UserId = userId,
                        CustomerName = checkoutDTO.CustomerName,
                        CustomerPhone = checkoutDTO.CustomerPhone,
                        DeleveryLocal = checkoutDTO.DeliveryAddress,
                        OrderDate = DateTime.Now,
                        DeliveryDate = DateTime.Now.AddDays(5), // Example delivery date
                        Total = checkoutDTO.OrderTotal,
                        Status = EStatus.Pending, // Assuming EStatus is an enum
                        OrderDetails = cartItems.Select(c => new OrderDetail
                        {
                            ProductId = c.ProductId,
                            Quantity = c.Quantity,
                            UnitPrice = c.Product.Price,
                        }).ToList()
                    };

                    // Save the order to the database
                    _orderRepository.AddOrder(order);

                    // Clear the user's cart
                    _cartRepository.ClearCart(userId);

                    foreach (var item in cartItems)
                    {
                        var product = item.Product;
                        product.SoldQuantity += item.Quantity;
                        _productRepository.UpdateProdct(product);
                    }

                    // Redirect to a confirmation page (or display success message)
                    return RedirectToAction("Index");
                }

                // If the cart is empty, show an error message
                ViewBag.ErrorMessage = "Your cart is empty!";

                return View("CheckOut");
            }

            checkoutDTO.CartItems = cartItems;
            return View(checkoutDTO);
        }
    }
}
