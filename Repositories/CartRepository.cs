using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Cart> GetCartItemsByUserId(string userId)
        {
            return _context.Carts.Include(c => c.Product).Where(c => c.UserId == userId).ToList();
        }

        public Cart GetCartByUserAndProduct(string userId, int productId)
        {
            return _context.Carts.Include(c => c.Product).FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }

        public void AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCart(Cart cart)
        {
            _context.Carts.Update(cart);
            _context.SaveChanges();
        }

        public void RemoveCartItem(Cart cart)
        {
            _context.Carts.Remove(cart);
            _context.SaveChanges();
        }

        public void ClearCart(string userId)
        {
            var cartItems = _context.Carts.Where(c => c.UserId == userId);
            _context.Carts.RemoveRange(cartItems);
            _context.SaveChanges();
        }
    }
}
