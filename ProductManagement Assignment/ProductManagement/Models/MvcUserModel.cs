using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
    public class MvcUserModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "required*")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "required*")]
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Required(ErrorMessage = "required*")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?#&])[A-Za-z\d@$!%*?#&]{8,15}$",
                            ErrorMessage = "Password should contain min 8 char and max 15 char and should have uppercase lowercase digits and special symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}