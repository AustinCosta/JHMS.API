using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventBounceHousesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventBounceHousesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		//Get Event Bouncehouses
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetAllEventBounceHouses([FromRoute] string id)
		{
			//Get the Event ID
			var intEventID = Int32.Parse(id);
			var dbevent = await _jhmsDbContext.TEvents.FirstOrDefaultAsync(x => x.intEventID == intEventID);

			//Check that event exists
			if (dbevent == null)
			{
				return NotFound();
			}

			//Get vehicles for the event ID passed in
			var eventBounceHouses = from TE in _jhmsDbContext.TEvents
									from TEB in _jhmsDbContext.TEventBounceHouses
									from TB in _jhmsDbContext.TBounceHouses
									where TE.intEventID == TEB.intEventID
									where TB.intBounceHouseID == TEB.intBounceHouseID
									where TE.intEventID == intEventID
									select new
									{
										intEventBounceHouseID = TEB.intEventBounceHouseID,
										intBounceHouseID = TB.intBounceHouseID,
										strBounceHouseName = TB.strBounceHouseName,
										intEmployeesNeededForSetup = TB.intEmployeesNeededForSetup,
										intNumberOfBlowersRequired = TB.intNumberOfBlowersRequired,
										intNumberOfStakesRequired = TB.intNumberOfStakesRequired
									};

			return Ok(eventBounceHouses);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEventBounceHouse ([FromRoute] string id)
		{
			var intEventBounceHouseID = Int32.Parse(id);
			var bounceHouse = await _jhmsDbContext.TEventBounceHouses.FindAsync(intEventBounceHouseID);

			if (bounceHouse == null)
			{
				return NotFound();
			}

			_jhmsDbContext.TEventBounceHouses.Remove(bounceHouse);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(intEventBounceHouseID);
		}

	}
}
