using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [StringLength(100, ErrorMessage = "Product Name cannot exceed 100 characters.")]
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [StringLength(255, ErrorMessage = "Short Description cannot exceed 255 characters.")]
        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Detail description")]
        public string DetailDescription { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        [Display(Name = "Upload image")]
        public IFormFile? ImageFile { get; set; }

        [Required]
        [Display(Name = "Is on sale")]
        public bool IsSale { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "CPU cannot exceed 100 characters.")]
        public string CPU { get; set; }

        [StringLength(50, ErrorMessage = "RAM cannot exceed 50 characters.")]
        public string RAM { get; set; }

        [StringLength(100, ErrorMessage = "Storage cannot exceed 100 characters.")]
        [Display(Name = "Graphics Card")]
        public string GraphicsCard { get; set; }

        [StringLength(50, ErrorMessage = "Screen Size cannot exceed 50 characters.")]
        [Display(Name = "Screen Size")]
        public string ScreenSize { get; set; }

        [StringLength(50, ErrorMessage = "Warranty Period cannot exceed 50 characters.")]
        [Display(Name = "Warranty Period")]
        public string WarrantyPeriod { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;

        [Display(Name = "Suplier")]
        public int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
