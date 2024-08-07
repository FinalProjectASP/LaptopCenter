using LaptopCenter.Data;
using LaptopCenter.DTO;
using LaptopCenter.Models;
using LaptopCenter.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LaptopCenter.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            try
            {
                return await _context.Suppliers.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error getting Supplier");
            }
        }

        public async Task<Supplier?> GetOneSupplier(int SupplierId)
        {
            try
            {
                Supplier supplier = await _context.Suppliers.FindAsync(SupplierId);

                if (supplier == null)
                {
                    throw new Exception("Not found Supplier");
                }
                return supplier;
            }
            catch (Exception)
            {

                throw new Exception("Error getting Supplier");
            }
        }
    }
}
