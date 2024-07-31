using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductsDTO>> GetProducts();
    }
}
