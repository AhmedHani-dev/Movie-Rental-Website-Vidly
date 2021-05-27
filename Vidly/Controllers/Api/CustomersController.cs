﻿using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
	public class CustomersController : ApiController
	{
		private ApplicationDbContext _context;

		public CustomersController()
		{
			_context = new ApplicationDbContext();
		}

		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}

		public IHttpActionResult GetCustomers(string query = null)
		{
			var customersQuery = _context.Customers
				.Include(c => c.MembershipType);

			if (!string.IsNullOrWhiteSpace(query))
				customersQuery = customersQuery.Where(c => c.Name.Contains(query));

			var customerDtos = customersQuery
				.ToList()
				.Select(Mapper.Map<Customer, CustomerDto>);
			return Ok(customerDtos);
		}

		public IHttpActionResult GetCustomer(int id)
		{
			var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
			if (customer == null)
				return NotFound();

			return Ok(Mapper.Map<Customer, CustomerDto>(customer));
		}

		[HttpPost]
		public IHttpActionResult CreateCustomer(CustomerDto customerDto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
			_context.Customers.Add(customer);
			_context.SaveChanges();

			customerDto.Id = customer.Id;
			return Created(new Uri(Request.RequestUri + "/" + customerDto.Id.ToString()), customerDto);
		}

		[HttpPut]
		public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
			if (customerInDb == null)
			{
				return NotFound();
			}

			Mapper.Map(customerDto, customerInDb);
			_context.SaveChanges();

			return Ok(customerDto);
		}

		[HttpDelete]
		public IHttpActionResult DeleteCustomer(int id)
		{
			var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
			if (customerInDb == null)
			{
				return NotFound();
			}

			_context.Customers.Remove(customerInDb);
			_context.SaveChanges();
			return Ok();
		}
	}
}
