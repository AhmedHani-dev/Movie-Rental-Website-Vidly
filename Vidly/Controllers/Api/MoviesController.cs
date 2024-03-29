﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
		private ApplicationDbContext _context;

		public MoviesController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public IHttpActionResult GetMovies(string query = null)
		{
			var movieQuery = _context.Movies
				.Include(m => m.Genre)
				.Where(m => m.NumberAvailable > 0);

			if (!string.IsNullOrWhiteSpace(query))
				movieQuery = movieQuery.Where(m => m.Name.Contains(query));

			var movieDtos = movieQuery
				.ToList()
				.Select(Mapper.Map<Movie, MovieDto>);
			return Ok(movieDtos);
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
		[Authorize(Roles = RoleName.CanManageMovies)]
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
		[Authorize(Roles = RoleName.CanManageMovies)]
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

		[HttpDelete]
		[Authorize(Roles = RoleName.CanManageMovies)]
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
