using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EmployeesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EmployeesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEmployees()
		{
			//Get a list of all employees
			var employees = await _jhmsDbContext.TEmployees.ToListAsync();

			//Return OK Response with customers data
			return Ok(employees);
		}

		[HttpPost]
		public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
		{
			await _jhmsDbContext.TEmployees.AddAsync(employeeRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(employeeRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetEmployee([FromRoute] string id)
		{
			var intEmployeeID = Int32.Parse(id);
			var employee = await _jhmsDbContext.TEmployees.FirstOrDefaultAsync(x => x.intEmployeeID == intEmployeeID);

			if (employee == null)
			{
				return NotFound();
			}

			return Ok(employee);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateEmployee([FromRoute] string id, Employee updateEmployeeRequest)
		{
			var intEmployeeID = Int32.Parse(id);
			var employee = await _jhmsDbContext.TEmployees.FindAsync(intEmployeeID);

			if (employee == null)
			{
				NotFound();
			}

			//Pass fields from request body to database table/entity
			employee.strFirstName = updateEmployeeRequest.strFirstName;
			employee.strLastName = updateEmployeeRequest.strLastName;
			employee.strPhoneNumber = updateEmployeeRequest.strPhoneNumber;
			employee.strAddress = updateEmployeeRequest.strAddress;
			employee.strCity = updateEmployeeRequest.strCity;
			employee.strState = updateEmployeeRequest.strState;
			employee.strZip = updateEmployeeRequest.strZip;
			employee.strDateOfBirth = updateEmployeeRequest.strDateOfBirth;
			employee.strEmail = updateEmployeeRequest.strEmail;
			employee.strDrivingStatus = updateEmployeeRequest.strDrivingStatus;
			employee.strEmploymentTitle = updateEmployeeRequest.strEmploymentTitle;

			await _jhmsDbContext.SaveChangesAsync();

			return Ok(employee);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEmployee([FromRoute] string id)
		{
			var intEmployeeID = Int32.Parse(id);
			var employee = await _jhmsDbContext.TEmployees.FindAsync(intEmployeeID);

			if (employee == null)
			{
				return NotFound();
			}

			//Foreign Keys
			//Delete from EventEmployees FIRST
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventEmployees WHERE intEmployeeID = {intEmployeeID}"); //Delete from EventEmployees
			await _jhmsDbContext.SaveChangesAsync();


			_jhmsDbContext.TEmployees.Remove(employee);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(employee);
		}
	}
}
