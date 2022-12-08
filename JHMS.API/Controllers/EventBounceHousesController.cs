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

		//Get Available BounceHouses
		[HttpGet]
		[Route("{strStartDate:}/{strEndDate:}")]
		public async Task<IActionResult> GetAvailableBounceHouses([FromRoute] string strStartDate, [FromRoute] string strEndDate)
		{
			//Get the Event ID
			var intEventID = Int32.Parse("1");
			var dbevent = await _jhmsDbContext.TEvents.FirstOrDefaultAsync(x => x.intEventID == intEventID);

			//Parse dates from string/url
			DateTime dteEventStartDate = DateTime.Parse(strStartDate);
			DateTime dteEventEndDate = DateTime.Parse(strEndDate);

			var allInflatables = from TB in _jhmsDbContext.TBounceHouses where TB.intBounceHouseTypeID == 1 
																					select new { 
																						intBounceHouseID = TB.intBounceHouseID, 
																						strBounceHouseName = TB.strBounceHouseName,
																						strBounceHouseType = TB.intBounceHouseTypeID
																					};

			//Get vehicles for the event ID passed in
			var eventBounceHouses = from TE in _jhmsDbContext.TEvents
									from TEB in _jhmsDbContext.TEventBounceHouses
									from TB in _jhmsDbContext.TBounceHouses
									from TBT in _jhmsDbContext.TBounceHouseTypes
									where TE.intEventID == TEB.intEventID
									where TB.intBounceHouseID == TEB.intBounceHouseID
									where TBT.intBounceHouseTypeID == TB.intBounceHouseTypeID
									where TE.dteEventStartDate >= dteEventStartDate && TE.dteEventEndDate <= dteEventEndDate
									select new
									{
										intBounceHouseID = TB.intBounceHouseID,
										strBounceHouseName = TB.strBounceHouseName,
										strBounceHouseType = TB.intBounceHouseTypeID
									};

			var available = allInflatables.Except(eventBounceHouses);

			return Ok(available);
		}

		[HttpPost]
		public async Task<IActionResult> AddBounceHouse([FromBody] EventBounceHouse addBounceHouseRequest)
		{
			await _jhmsDbContext.TEventBounceHouses.AddAsync(addBounceHouseRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(addBounceHouseRequest);
		}

	}
}
