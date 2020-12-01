using System;
using System.ComponentModel.DataAnnotations;

namespace SourceControlAssignment1.CustomValidation
{
    public class CustomDate: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}