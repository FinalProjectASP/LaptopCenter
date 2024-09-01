using LaptopCenter.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCenter.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public string DeleveryLocal { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhone { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        [Required]
        public EStatus Status { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
}
