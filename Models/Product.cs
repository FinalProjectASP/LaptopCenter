using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [StringLength(100, ErrorMessage = "Product Name cannot exceed 100 characters.")]
        public string ProductName { get; set; }

        [StringLength(255, ErrorMessage = "Short Description cannot exceed 255 characters.")]
        public string ShortDescription { get; set; }

        public string DetailDescription { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [Required]
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
        public string GraphicsCard { get; set; }

        [StringLength(50, ErrorMessage = "Screen Size cannot exceed 50 characters.")]
        public string ScreenSize { get; set; }

        [StringLength(50, ErrorMessage = "Warranty Period cannot exceed 50 characters.")]
        public string WarrantyPeriod { get; set; }

        public DateTime CreateAt { get; set; }

        public virtual int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        public virtual int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
