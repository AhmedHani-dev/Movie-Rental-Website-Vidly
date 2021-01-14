using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.ValidationModels;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		
		[Min18YearsIfAMember]
		public DateTime? BirthDate { get; set; }
		
		public bool IsSubscribedToNewsLetter { get; set; }
		
		public MembershipType MembershipType { get; set; }
		
		[Required]
		public byte MembershipTypeId { get; set; }
	}
}