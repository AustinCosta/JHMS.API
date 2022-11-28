using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class BounceHousesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public BounceHousesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllBounceHouses()
		{
			//Get a list of all customers from TCustomers
			var bouncehouses = await _jhmsDbContext.TBounceHouses.ToListAsync();

			//Return OK Response with customers data
			return Ok(bouncehouses);
		}

		[HttpPost]
		public async Task<IActionResult> AddBounceHouse([FromBody] BounceHouse bounceHouseRequest)
		{
			await _jhmsDbContext.TBounceHouses.AddAsync(bounceHouseRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(bounceHouseRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetBounceHouse([FromRoute] string id)
		{
			var intBounceHouseID = Int32.Parse(id);
			var bounceHouse = await _jhmsDbContext.TBounceHouses.FirstOrDefaultAsync(x => x.intBounceHouseID == intBounceHouseID);

			if (bounceHouse == null)
			{
				return NotFound();
			}

			return Ok(bounceHouse);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateBounceHouse([FromRoute] string id, BounceHouse updateBounceHouseRequest)
		{
			var intBounceHouseID = Int32.Parse(id);
			var bounceHouse = await _jhmsDbContext.TBounceHouses.FindAsync(intBounceHouseID);

			if (bounceHouse == null)
			{
				NotFound();
			}

			bounceHouse.intBounceHouseTypeID = updateBounceHouseRequest.intBounceHouseTypeID;
			bounceHouse.strBounceHouseName = updateBounceHouseRequest.strBounceHouseName;
			bounceHouse.strDescription = updateBounceHouseRequest.strDescription;
			bounceHouse.intEmployeesNeededForSetup = updateBounceHouseRequest.intEmployeesNeededForSetup;
			bounceHouse.intNumberOfStakesRequired = updateBounceHouseRequest.intNumberOfStakesRequired;
			bounceHouse.intNumberOfBlowersRequired = updateBounceHouseRequest.intNumberOfBlowersRequired;
			bounceHouse.strDateOfLastPurchase = updateBounceHouseRequest.strDateOfLastPurchase;
			bounceHouse.strURL = updateBounceHouseRequest.strURL;
			bounceHouse.intPurchaseYear = updateBounceHouseRequest.intPurchaseYear;

			await _jhmsDbContext.SaveChangesAsync();

			return Ok(bounceHouse);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteBounceHouse([FromRoute] string id)
		{
			var intBounceHouseID = Int32.Parse(id);
			var bounceHouse = await _jhmsDbContext.TBounceHouses.FindAsync(intBounceHouseID);

			if (bounceHouse == null)
			{
				return NotFound();
			}

			//Remove/drop foreign keys first -- EventVehicles
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventBounceHouses WHERE intBounceHouseID = {intBounceHouseID}"); //Delete from EventBounceHouses
			await _jhmsDbContext.SaveChangesAsync();

			_jhmsDbContext.TBounceHouses.Remove(bounceHouse);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(bounceHouse);
		}
	}
}
