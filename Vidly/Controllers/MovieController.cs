using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.App_Start;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class MovieController : Controller
	{
		private MyDbContext _context;
		public MovieController()
		{
			_context = new MyDbContext();
		}
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
		// GET: Movie
		public ActionResult Index()
		{
			var movies = _context.Movies.Include(c => c.Genre).ToList();
			return View(movies);
		}

		[Route("Movie/Details/{Id}")]
		public ActionResult Details(int Id)
		{
			var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == Id);
			return View(movie);
		}
	}
}