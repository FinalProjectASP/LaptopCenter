using System;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.DTO
{
    public class CategoriesDTO
    {
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        
    }
}
