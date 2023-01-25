using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SchoolDatabase.Attributes
{
    public class NeptunCodeValidationAttribute : ValidationAttribute
    {
        private readonly static int NEPTUNCODE_LENGTH = 6;

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is string neptunCode)
            {
                string pattern = @"^[a-zA-Z]{1}[a-zA-Z0-9]{5}$";
                var match = Regex.IsMatch(neptunCode, pattern);

                if(match)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Wrong Neptun code!");
            }
            
            return new ValidationResult("Wrong Neptun code!");
        }
    }
}
