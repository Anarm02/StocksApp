using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ValidationAttributes
{
	public class DateAfterAttribute:ValidationAttribute
	{
		private readonly DateTime _date;
        public DateAfterAttribute(string minDate)
        {
            _date=DateTime.Parse(minDate);
            ErrorMessage = $"Date must be on or after {_date.ToShortDateString()}.";

		}
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateTime datevalue && datevalue < _date) {

				return new ValidationResult(ErrorMessage);
			}
			return ValidationResult.Success;
		}
	}
}
