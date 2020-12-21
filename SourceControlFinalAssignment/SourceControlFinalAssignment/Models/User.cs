using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SourceControlFinalAssignment.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Email Address")]
        public string UserEmail { get; set; }
        
        [Display(Name = "Phone Number")]
        public string UserPhone { get; set; }
       
        [Display(Name = "Password")]
        public string UserPassword { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UserBirthDate { get; set; }
        
        //[Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name ="Profile Image")]
        public string UserImage { get; set; }
    }
}