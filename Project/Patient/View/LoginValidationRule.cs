using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Patient.View
{
    public class USernameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String input = (String)value;
            if (input.Equals(""))
            {
                return new ValidationResult(false, "Morate uneti podatke");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class PasswordValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String input = (String)value;
            if (input.Equals(""))
            {
                return new ValidationResult(false, "Morate uneti podatke");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
