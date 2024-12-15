using LaptopCenter.DTO;
using LaptopCenter.Models;

namespace LaptopCenter.Repositories.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetOneSupplier(int supplierId);
    }
}
