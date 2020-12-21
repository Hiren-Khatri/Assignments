using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SourceControlFinalAssignment.CustomValidation;

namespace SourceControlFinalAssignment.Models
{  
    public class UserView
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="required*")]
        [StringLength(20)]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "required*")]
        [Display (Name ="Email Address")]
        public string UserEmail { get; set; }

        [Phone]
        [Required(ErrorMessage = "required*")]
        [RegularExpression(@"^(\+?\d{1,4}[\s-])?(?!0+\s+,?$)\d{10}\s*,?$", ErrorMessage ="Enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string UserPhone { get; set; }


        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?#&])[A-Za-z\d@$!%*?#&]{8,15}$",
                            ErrorMessage = "Password should contain min 8 char and max 15 char and should have uppercase lowercase digits and special symbols")]
        [Required(ErrorMessage = "required*")]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "required*")]
        [Compare("UserPassword")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ReType { get; set; }

        [Required(ErrorMessage = "required*")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomDate(ErrorMessage = "Birth Date must be less than Today's Date")]
        public DateTime UserBirthDate { get; set; }

        [DataType(DataType.Upload)]
        //[Required(ErrorMessage ="required*")]
        [Display(Name ="Profile Photo")]
        public HttpPostedFileBase UserImage { get; set; }
    }
}