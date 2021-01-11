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

		[Display(Name = "Release Date")]
		public DateTime ReleaseDate { get; set; }
		
		public DateTime DateAdded { get; set; }

		[Display(Name = "Number in Stock")]
		public int NumberInStock { get; set; }
		
		public Genre Genre { get; set; }

		[Display(Name = "Genre")]
		public byte GenreId { get; set; }
	}
}