using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ValidationModels
{
	public class Min18YearsIfAMember : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var customer = (Customer)validationContext.ObjectInstance;
			
			if (customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
				return ValidationResult.Success;
			
			if (customer.BirthDate == null)
				return new ValidationResult("BirthDate is required.");
			
			var today = DateTime.Today;
			var age = today.Year - customer.BirthDate.Value.Year;
			if (customer.BirthDate.Value.Date > today.AddYears(-age))	//in case of leap year
				--age;
			
			return age >= 18 ? ValidationResult.Success : new ValidationResult("Customer should be at least 18 years old.");
		}
	}
}