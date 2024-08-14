using LaptopCenter.Models;

namespace LaptopCenter.DTO
{
    public class CheckoutDTO
    {
        public List<Cart> CartItems { get; set; }
        public decimal OrderTotal { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }

}
