using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.App_Start;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
	public class MoviesController : Controller
	{
		private MyDbContext _context;
		
		public MoviesController()
		{
			_context = new MyDbContext();
		}
		
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult New()
		{
			var viewModel = new MovieFormViewModel
			{
				Genres = _context.Genres.ToList()
			};
			return View("MovieForm", viewModel);
		}

		public ActionResult Edit(int id)
		{
			var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
			if (movie == null)
				return HttpNotFound();
			var viewModel = new MovieFormViewModel(movie)
			{
				Genres = _context.Genres.ToList()
			};
			return View("MovieForm", viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Save(Movie movie)
		{
			if (!ModelState.IsValid) {
				var viewModel = new MovieFormViewModel(movie)
				{
					Genres = _context.Genres.ToList()
				};
				return View("MovieForm", viewModel);
			}
			if (movie.Id == 0)
				_context.Movies.Add(movie);
			else
			{
				var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate = movie.ReleaseDate;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.NumberInStock = movie.NumberInStock;
			}
			_context.SaveChanges();
			return RedirectToAction("Index", "Movies");
		}

		[Route("Movie/Details/{Id}")]
		public ActionResult Details(int Id)
		{
			var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == Id);
			return View(movie);
		}
	}
}