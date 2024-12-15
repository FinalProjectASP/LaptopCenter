using LaptopCenter.Models;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.DTO
{
    public class ReviewsDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string UserId { get; set; }
    }
}
