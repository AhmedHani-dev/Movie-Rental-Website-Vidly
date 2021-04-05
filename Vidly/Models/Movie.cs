using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidly.Models
{
	public class Movie
	{
		public Movie()
		{
			DateAdded = DateTime.Now;
		}

		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public DateTime ReleaseDate { get; set; }
		
		[Required]
		public DateTime DateAdded { get; set; }

		[Required]
		public int NumberInStock { get; set; }
		
		[Required]
		public int NumberAvailable { get; set; }

		public Genre Genre { get; set; }

		[Required]
		public byte GenreId { get; set; }
	}
}