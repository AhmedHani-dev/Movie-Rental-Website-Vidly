using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
	public class CustomersController : Controller
	{
		List<Customer> customers = new List<Customer>
		{
			new Customer { Name = "John Smith", Id = 1},
			new Customer { Name = "Mary Williams", Id = 2}
		};
		// GET: Customers
		public ActionResult Index()
		{

			return View(customers);
		}

		[Route("Details/{Id}")]
		public ActionResult Details(int id, string name)
		{
			foreach (var customer in customers)
				if (customer.Id == id)
					return View(customer);

			return HttpNotFound();
		}
	}
}