using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            try
            {
                return await _context.Categories.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error getting Category");
            }
        }

        public async Task<Category?> GetOneCategory(int categoryId)
        {
            try
            {
                Category category = await _context.Categories.FindAsync(categoryId);

                if (category == null)
                {
                    throw new Exception("Not found Category");
                }
                return category;
            }
            catch (Exception)
            {

                throw new Exception("Error getting Category");
            }
        }
    }
}
