using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.Models
{
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int OrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal UnitPrice { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }
    }
}
