using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCenter.Models
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierId { get; set; }


        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Supplier Name cannot exceed 100 characters.")]
        public string SupplierName { get; set; }


        [Display(Name = "Logo")]
        public string? LogoUrl{ get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? LogoFile { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
