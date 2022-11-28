using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class VehiclesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public VehiclesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllVehicles()
		{
			//Get a list of all customers from TCustomers
			var vehicles = await _jhmsDbContext.TVehicles.ToListAsync();

			//Return OK Response with customers data
			return Ok(vehicles);
		}

		[HttpPost]
		public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicleRequest)
		{
			await _jhmsDbContext.TVehicles.AddAsync(vehicleRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(vehicleRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetVehicle([FromRoute] string id)
		{
			var intVehicleID = Int32.Parse(id);
			var vehicle = await _jhmsDbContext.TVehicles.FirstOrDefaultAsync(x => x.intVehicleID == intVehicleID);

			if (vehicle == null)
			{
				return NotFound();
			}

			return Ok(vehicle);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateVehicle([FromRoute] string id, Vehicle updateVehicleRequest)
		{
			var intVehicleID = Int32.Parse(id);
			var vehicle = await _jhmsDbContext.TVehicles.FindAsync(intVehicleID);

			if (vehicle == null)
			{
				NotFound();
			}

			vehicle.strVehicleName = updateVehicleRequest.strVehicleName;
			vehicle.intQuantity = updateVehicleRequest.intQuantity;

			await _jhmsDbContext.SaveChangesAsync();

			return Ok(vehicle);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteVehicle([FromRoute] string id)
		{
			var intVehicleID = Int32.Parse(id);
			var vehicle = await _jhmsDbContext.TVehicles.FindAsync(intVehicleID);

			if (vehicle == null)
			{
				return NotFound();
			}

			//Remove/drop foreign keys first -- EventVehicles
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventVehicles WHERE intVehicleID = {intVehicleID}"); //Delete from EventVehicles
			await _jhmsDbContext.SaveChangesAsync();

			_jhmsDbContext.TVehicles.Remove(vehicle);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(vehicle);
		}
	}
}
