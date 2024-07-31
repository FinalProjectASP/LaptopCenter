using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace LaptopCenter.Repository
{
    public class ProductRepository : IProductRepository
    {

        public async Task<IEnumerable<ProductsDTO>> GetProducts()
        {
            using (var context = new ApplicationDbContext())
            {
                List<ProductsDTO> products = context.Products
                    .Include(p => p.ProductId)
                    .Include(p => p.ProductName)
                    .Include(p => p.ShortDescription)
                    .Include(p => p.Price)
                    .Select(product => new ProductsDTO
                    {
                        
                    })
                    .ToList();

                return products;
            }
        }
    }
}
