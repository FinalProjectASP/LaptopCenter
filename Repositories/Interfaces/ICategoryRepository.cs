using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetOneCategory(int categoryId);
    }
}
