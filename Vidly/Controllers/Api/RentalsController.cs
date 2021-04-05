using AutoMapper;
using System;
using System.Linq;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class RentalsController : ApiController
	{
		private ApplicationDbContext _context;

		public RentalsController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		[HttpPost]
		public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
		{
			var customer = _context.Customers.Single(c => c.Id == newRental.CustomerId);
			var movie = _context.Movies.Single(m => m.Id == newRental.MovieId);
			if (movie.NumberAvailable == 0)
				return BadRequest("Movie is not available.");
			movie.NumberAvailable--;
			var rental = new Rental
			{
				Customer = customer,
				Movie = movie,
				DateRented = DateTime.Now
			};
			_context.Rentals.Add(rental);
			_context.SaveChanges();

			return Ok();
		}
	}
}