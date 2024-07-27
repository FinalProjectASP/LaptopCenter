using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCenter.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
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
        public decimal Total { get; set; }
        [Required]
        public Boolean IsConfirm { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
