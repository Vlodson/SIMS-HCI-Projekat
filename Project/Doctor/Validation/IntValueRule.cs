using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Doctor.Validation
{
    public class IntValueRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var valueInt = (string)value;
            Regex regex = new Regex("[0-9]+$");
            if (regex.Match(valueInt).Success)
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Morate da unesete broj!");
            }

        }
    }
}
