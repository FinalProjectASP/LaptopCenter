using LaptopCenter.Models;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.DTO
{
    public class CheckoutDTO
    {
        public List<Cart> CartItems { get; set; }
        public decimal OrderTotal { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
    }

}
