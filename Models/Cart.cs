using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
