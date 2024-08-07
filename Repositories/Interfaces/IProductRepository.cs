using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductsDTO>> GetProducts(string status);
        Task<ProductsDTO> GetOneProduct(int productId);
        Task<IEnumerable<ProductsDTO>> GetTopSellingProducts();
        Task<IEnumerable<ProductsDTO>> GetLatestProducts();
        Task<IEnumerable<ProductsDTO>> GetSuggestProducts(string supplierName);
        string SaveProduct(Product product);
    }
}
