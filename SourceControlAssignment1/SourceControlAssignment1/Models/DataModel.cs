using SourceControlAssignment1.CustomValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SourceControlAssignment1.Models
{
    public class DataModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Name not be exceed from 20")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,10}$",
                            ErrorMessage = "Password should contain min 8 char and max 10 char and should have uppercase lowercase digits and special symbols")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name ="ReType Password")]
        public string ReType { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [MinLength(10, ErrorMessage = "Phone number must be 10 digit long")]
        public string Phone { get; set; }


        [Required]
        [Range(18, 60, ErrorMessage = "Age should be between 18 and 60")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [CustomDate(ErrorMessage = "Hire Date must be less than or equal to Today's Date")]
        public DateTime HireDate { get; set; }

        [Required]
        [Display(Name = "Credit card number")]
        [CreditCard(ErrorMessage = "Invalid Credit card")]
        public string Card { get; set; }


        [Required]
        [DataType(DataType.Upload)]
        [FileExtensions]
        [Display(Name ="Select Image")]
        public string File { get; set; }
    }
}