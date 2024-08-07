using System;
using System.ComponentModel.DataAnnotations;

namespace LaptopCenter.DTO
{
    public class SuppliersDTO
    {
        [Display(Name = "Supplier ID")]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        
    }
}
