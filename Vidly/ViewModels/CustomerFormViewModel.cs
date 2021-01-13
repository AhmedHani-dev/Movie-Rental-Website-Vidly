using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;
using Vidly.ValidationModels;

namespace Vidly.ViewModels
{
	public class CustomerFormViewModel
	{
		public IEnumerable<MembershipType> MembershipTypes { get; set; }

		public int? Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[Min18YearsIfAMember]
		[Display(Name = "Date of Birth")]
		public DateTime? BirthDate { get; set; }

		public bool IsSubscribedToNewsLetter { get; set; }
		
		[Required]
		[Display(Name = "Membership Type")]
		public byte? MembershipTypeId { get; set; }

		public string Title{
			get
			{
				return Id != 0 ? "Edit Customer" : "New Customer";
			}
		}

		public CustomerFormViewModel()
		{
			Id = 0;
		}

		public CustomerFormViewModel(Customer customer)
		{
			Id = customer.Id;
			Name = customer.Name;
			BirthDate = customer.BirthDate;
			IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
			MembershipTypeId = customer.MembershipTypeId;
		}
	}
}