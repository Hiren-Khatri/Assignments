using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagement.Models
{
    public class MvcProductModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Value must be greater than 1.")]
        public double Price { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value must be greater than 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "required*")]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Short Description")]
        public string ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string LongDescription { get; set; }

        
        [Display(Name = "Small Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase SmallImageFile { get; set; }

        [Display(Name = "Large Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase LargeImageFile { get; set; }

        
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string SmallImage { get; set; }
        public string LargeImage { get; set; }
        //public IEnumerable<MvcProductCategoryModel> productCategories { get; set; }
    }
}