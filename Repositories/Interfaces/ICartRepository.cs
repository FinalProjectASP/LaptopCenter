using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<Cart> GetCartItemsByUserId(string userId);
        Cart GetCartByUserAndProduct(string userId, int productId);
        void AddToCart(Cart cart);
        void UpdateCart(Cart cart);        
        void RemoveCartItem(Cart cart);
        void ClearCart(string userId);
    }
}
