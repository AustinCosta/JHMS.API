using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class CustomersController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public CustomersController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCustomers()
		{
			//Get a list of all customers from TCustomers
			var customers = await _jhmsDbContext.TCustomers.ToListAsync();

			//Return OK Response with customers data
			return Ok(customers);
		}

		[HttpPost]
		public async Task<IActionResult> AddCustomer([FromBody] Customer customerRequest)
		{
			await _jhmsDbContext.TCustomers.AddAsync(customerRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(customerRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetCustomer([FromRoute] string id)
		{
			var intCustomerID = Int32.Parse(id);
			var customer = await _jhmsDbContext.TCustomers.FirstOrDefaultAsync(x => x.intCustomerID == intCustomerID);

			if(customer == null)
			{
				return NotFound();
			}

			return Ok(customer);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateCustomer([FromRoute] string id, Customer updateCustomerRequest)
		{
			var intCustomerID = Int32.Parse(id);
			var customer = await _jhmsDbContext.TCustomers.FindAsync(intCustomerID);

			if(customer == null)
			{
				NotFound();
			}

			customer.strFirstName = updateCustomerRequest.strFirstName;
			customer.strLastName = updateCustomerRequest.strLastName;
			customer.strAddress = updateCustomerRequest.strAddress; 
			customer.strCity = updateCustomerRequest.strCity;
			customer.strState = updateCustomerRequest.strState;
			customer.strZip = updateCustomerRequest.strZip;
			customer.strEmail = updateCustomerRequest.strEmail;


			await _jhmsDbContext.SaveChangesAsync();

			return Ok(customer);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteCustomer([FromRoute] string id)
		{
			var intCustomerID = Int32.Parse(id);
			var customer = await _jhmsDbContext.TCustomers.FindAsync(intCustomerID);

			if(customer == null)
			{ 
				return NotFound();
			}

			_jhmsDbContext.TCustomers.Remove(customer);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(customer);
		}

	}
}
