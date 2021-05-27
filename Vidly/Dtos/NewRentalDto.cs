using System.Collections.Generic;

namespace Vidly.Models
{
	public class NewRentalDto
	{
		public int CustomerId { get; set; }

		public List<int> MoviesIds { get; set; }
	}
}