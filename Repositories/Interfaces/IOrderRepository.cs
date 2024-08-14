using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);
        Order GetOrderById(int orderId);
        List<OrderDTO> GetHistoryPurchase(string userId);
        List<OrderDTO> GetCurrentOrder(string userId);
    }

}
