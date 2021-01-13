using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
	public class MovieFormViewModel
	{
		public IEnumerable<Genre> Genres { get; set; }

		public int? Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Release Date")]
		public DateTime? ReleaseDate { get; set; }

		[Required]
		[Range(1, 20, ErrorMessage = "The Field Number in Stock must be between 1 and 20.")]
		[Display(Name = "Number in Stock")]
		public int? NumberInStock { get; set; }

		[Required]
		[Display(Name = "Genre")]
		public byte? GenreId { get; set; }

		public string Title
		{
			get
			{
				return Id != 0 ? "Edit Movie" : "New Movie";
			}
		}

		public MovieFormViewModel()
		{
			Id = 0;
		}

		public MovieFormViewModel(Movie movie)
		{
			Id = movie.Id;
			Name = movie.Name;
			ReleaseDate = movie.ReleaseDate;
			NumberInStock = movie.NumberInStock;
			GenreId = movie.GenreId;
		}
	}
}