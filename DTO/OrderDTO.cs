using LaptopCenter.Models;

namespace LaptopCenter.DTO
{
    public class OrderDTO
    {
        public Order Order { get; set; }
        public List<Details> Details { get; set; }
    }

    public class Details
    {
        public ProductsDTO Product { get; set; }
        public OrderDetailDTO OrderDetail { get; set; }
        public ReviewsDTO Reviews { get; set; }
    }
}
