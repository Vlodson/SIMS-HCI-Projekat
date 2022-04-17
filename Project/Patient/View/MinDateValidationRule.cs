using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Patient.View
{
    public class MinDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime selected = (DateTime)value;
            if (selected.CompareTo(DateTime.Now) < 0)
            {
                return new ValidationResult(false, "Morate izabrati datum nakon danasnjeg");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    public class MaxDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime selected = (DateTime)value;
            if (selected.CompareTo(DateTime.Now.AddDays(7)) > 0)
            {
                return new ValidationResult(false, "Ne mozete zakazati vise od sedam dana unapred");
            }
            else if(selected.CompareTo(AddExamination.startDate) < 0)
            {
                return new ValidationResult(false, "Krajnji datum ne moze biti pre pocetnog");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
