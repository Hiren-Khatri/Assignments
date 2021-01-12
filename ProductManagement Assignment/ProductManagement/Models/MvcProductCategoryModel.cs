using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class MvcProductCategoryModel
    {
        [Display(Name="Category Id")]
        public int Id { get; set; }

        [Required(ErrorMessage ="required*")]
        [Display(Name="Category Name")]
        public string Name { get; set; }
    }
}