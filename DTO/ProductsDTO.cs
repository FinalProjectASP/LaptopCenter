using LaptopCenter.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaptopCenter.DTO
{
    public class ProductsDTO
    {
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Detail Description")]
        public string? DetailDescription { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Display(Name = "Is on Sale")]
        public bool? IsSale { get; set; }

        [Display(Name = "CPU")]
        public string? CPU { get; set; }

        [Display(Name = "RAM")]
        public string? RAM { get; set; }

        [Display(Name = "Graphics Card")]
        public string? GraphicsCard { get; set; }

        [Display(Name = "Screen Size")]
        public string? ScreenSize { get; set; }

        [Display(Name = "Warranty Period")]
        public string? WarrantyPeriod { get; set; }

        [Display(Name = "Created At")]
        public DateTime? CreateAt { get; set; }

        [Display(Name = "Image")]
        public string? ProductImage { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Category Name")]
        public Category? Category { get; set; }

        [Display(Name = "Supplier Name")]
        public Supplier? Supplier { get; set; }

        [Display(Name = "Average Rate")]
        public double AverageRate { get; set; }
    }
}
