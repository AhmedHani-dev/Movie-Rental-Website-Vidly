using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.App_Start;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class MoviesController : ApiController
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

		public IHttpActionResult GetMovies()
		{
			return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
		}

		public IHttpActionResult GetMovie(int Id)
		{
			var movie = _context.Movies.Single(m => m.Id == Id);
			if (movie == null)
			{
				return NotFound();
			}
			return Ok(Mapper.Map<Movie, MovieDto>(movie));
		}

		[HttpPost]
		public IHttpActionResult AddMovie(MovieDto movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var movie = Mapper.Map<MovieDto, Movie>(movieDto);
			_context.Movies.Add(movie);
			_context.SaveChanges();

			movieDto.Id = movie.Id;
			return Created(new Uri(Request.RequestUri + "/" + movieDto.Id.ToString()), movieDto);
		}

		[HttpPut]
		public IHttpActionResult UpdateMovie(int Id, MovieDto movieDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var movieInDb = _context.Movies.Single(m => m.Id == Id);
			if(movieInDb == null)
			{
				return NotFound();
			}

			Mapper.Map(movieDto, movieInDb);
			_context.SaveChanges();

			return Ok(movieDto);
		}

		public IHttpActionResult DeleteMovie(int Id)
		{
			var movieInDb = _context.Movies.Single(m => m.Id == Id);
			if(movieInDb == null)
			{
				return NotFound();
			}

			_context.Movies.Remove(movieInDb);
			_context.SaveChanges();
			return Ok();
		}
	}
}
