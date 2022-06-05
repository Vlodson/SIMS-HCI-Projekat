using Patient.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Patient.Views
{
    public class MaxDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime selected = (DateTime)value;
            if (selected.CompareTo(DateTime.Now.AddDays(10)) > 0)
            {
                return new ValidationResult(false, "Ne mozete zakazati vise od deset dana unapred");
            }
            else if (selected.CompareTo(AddExaminationViewModel.startDate) < 0)
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
