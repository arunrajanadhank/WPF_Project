using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BitcoinPriceViewer.Model
{
    internal class EndDateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            DateTime date = (DateTime)value;

            return new ValidationResult(date < DateTime.Now, "Please, enter date before Now()");
        }
    }
}
