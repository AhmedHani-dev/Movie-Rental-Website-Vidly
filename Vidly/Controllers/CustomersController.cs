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
	public class CustomersController : Controller
	{
		private MyDbContext _context;
		public CustomersController()
		{
			_context = new MyDbContext();
		}
		protected override void Dispose(bool disposing)
		{
			_context.Dispose();
		}
		// GET: Customers
		public ActionResult Index()
		{
			var customers = _context.Customers.Include(c => c.MembershipType).ToList();
			//can be _context.Customers.Include("MemberShipType").ToList();
			return View(customers);
		}

		[Route("Details/{Id}")]
		public ActionResult Details(int id, string name)
		{
			var customers = _context.Customers;
			foreach (var customer in customers)
				if (customer.Id == id)
					return View(customer);

			return HttpNotFound();
		}
	}
}