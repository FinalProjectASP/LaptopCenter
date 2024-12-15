using LaptopCenter.Constraints;
using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<OrderDTO> GetHistoryPurchase(string userId)
        {
            var orders = (from order in _context.Orders
                          join orderDetail in _context.OrderDetails on order.OrderId equals orderDetail.OrderId
                          join product in _context.Products on orderDetail.ProductId equals product.ProductId
                          from review in _context.Reviews
                              .Where(r => r.ProductId == product.ProductId && r.OrderId == order.OrderId)
                              .DefaultIfEmpty()
                          where order.UserId == userId && order.Status == EStatus.Completed
                          select new
                          {
                              Order = order,
                              OrderDetail = new OrderDetailDTO
                              {
                                  ProductId = orderDetail.ProductId,
                                  OrderId = orderDetail.OrderId,
                                  Quantity = orderDetail.Quantity,
                                  UnitPrice = orderDetail.UnitPrice
                              },
                              Product = new ProductsDTO
                              {
                                  ProductId = product.ProductId,
                                  ProductName = product.ProductName,
                                  ShortDescription = product.ShortDescription,
                                  DetailDescription = product.DetailDescription,
                                  Quantity = product.Quantity,
                                  IsSale = product.IsSale,
                                  CPU = product.CPU,
                                  RAM = product.RAM,
                                  GraphicsCard = product.GraphicsCard,
                                  ScreenSize = product.ScreenSize,
                                  WarrantyPeriod = product.WarrantyPeriod,
                                  CreateAt = product.CreateAt,
                                  ProductImage = product.Image,
                                  Price = product.Price,
                                  Category = product.Category,
                                  Supplier = product.Supplier
                              },
                              Review = review != null ? new ReviewsDTO
                              {
                                  Id = review.Id,
                                  Comment = review.Comment,
                                  Date = review.Date,
                                  Rate = review.Rate,
                                  UserId = review.UserId
                              } : null
                          }).ToList();

            var result = orders
                .GroupBy(o => o.Order.OrderId)
                .Select(g => new OrderDTO
                {
                    Order = g.First().Order,
                    Details = g.Select(o => new Details
                    {
                        Product = o.Product,
                        OrderDetail = o.OrderDetail,
                        Reviews = o.Review
                    }).ToList()
                }).ToList();

            return result;
        }


        public List<OrderDTO> GetCurrentOrder(string userId)
        {
            // LINQ query to fetch current orders
            var orders = (from order in _context.Orders
                          join orderDetail in _context.OrderDetails on order.OrderId equals orderDetail.OrderId
                          join product in _context.Products on orderDetail.ProductId equals product.ProductId
                          from review in _context.Reviews
                              .Where(r => r.ProductId == product.ProductId && r.OrderId == order.OrderId)
                              .DefaultIfEmpty()
                          where order.UserId == userId && (order.Status == EStatus.Pending || order.Status == EStatus.Processing)
                          select new
                          {
                              Order = order,
                              OrderDetail = new OrderDetailDTO
                              {
                                  ProductId = orderDetail.ProductId,
                                  OrderId = orderDetail.OrderId,
                                  Quantity = orderDetail.Quantity,
                                  UnitPrice = orderDetail.UnitPrice
                              },
                              Product = new ProductsDTO
                              {
                                  ProductId = product.ProductId,
                                  ProductName = product.ProductName,
                                  ShortDescription = product.ShortDescription,
                                  DetailDescription = product.DetailDescription,
                                  Quantity = product.Quantity,
                                  IsSale = product.IsSale,
                                  CPU = product.CPU,
                                  RAM = product.RAM,
                                  GraphicsCard = product.GraphicsCard,
                                  ScreenSize = product.ScreenSize,
                                  WarrantyPeriod = product.WarrantyPeriod,
                                  CreateAt = product.CreateAt,
                                  ProductImage = product.Image,
                                  Price = product.Price,
                                  Category = product.Category,
                                  Supplier = product.Supplier
                              }
                          }).ToList();

            // Group by OrderId to assemble the final list of OrderDTOs
            var result = orders
                .GroupBy(o => o.Order.OrderId)
                .Select(g => new OrderDTO
                {
                    Order = g.First().Order, // Assign the Order object
                    Details = g.Select(o => new Details
                    {
                        Product = o.Product, // Assign the ProductDTO
                        OrderDetail = o.OrderDetail // Assign the OrderDetailDTO
                    }).ToList()
                }).ToList();

            return result;
        }


        public Order GetOrderById(int orderId)
        {
            return _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<OrderDTO> GetAllOrder()
        {
            // LINQ query to fetch current orders
            var orders = (from order in _context.Orders
                          join orderDetail in _context.OrderDetails on order.OrderId equals orderDetail.OrderId
                          join product in _context.Products on orderDetail.ProductId equals product.ProductId
                          from review in _context.Reviews
                              .Where(r => r.ProductId == product.ProductId && r.OrderId == order.OrderId)
                              .DefaultIfEmpty()
                          select new
                          {
                              Order = order,
                              OrderDetail = new OrderDetailDTO
                              {
                                  ProductId = orderDetail.ProductId,
                                  OrderId = orderDetail.OrderId,
                                  Quantity = orderDetail.Quantity,
                                  UnitPrice = orderDetail.UnitPrice
                              },
                              Product = new ProductsDTO
                              {
                                  ProductId = product.ProductId,
                                  ProductName = product.ProductName,
                                  ShortDescription = product.ShortDescription,
                                  DetailDescription = product.DetailDescription,
                                  Quantity = product.Quantity,
                                  IsSale = product.IsSale,
                                  CPU = product.CPU,
                                  RAM = product.RAM,
                                  GraphicsCard = product.GraphicsCard,
                                  ScreenSize = product.ScreenSize,
                                  WarrantyPeriod = product.WarrantyPeriod,
                                  CreateAt = product.CreateAt,
                                  ProductImage = product.Image,
                                  Price = product.Price,
                                  Category = product.Category,
                                  Supplier = product.Supplier
                              }
                          }).ToList();

            // Group by OrderId to assemble the final list of OrderDTOs
            var result = orders
                .GroupBy(o => o.Order.OrderId)
                .Select(g => new OrderDTO
                {
                    Order = g.First().Order, // Assign the Order object
                    Details = g.Select(o => new Details
                    {
                        Product = o.Product, // Assign the ProductDTO
                        OrderDetail = o.OrderDetail // Assign the OrderDetailDTO
                    }).ToList()
                }).ToList();

            return result;
        }
    }
}
