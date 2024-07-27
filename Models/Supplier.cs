using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCenter.Models
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }

        [StringLength(100, ErrorMessage = "Supplier Name cannot exceed 100 characters.")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Logo URL is required.")]
        public string? LogoUrl{ get; set; }

        [NotMapped]
        public IFormFile? LogoFile { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
