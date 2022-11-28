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
										intBounceHouseID = TB.intBounceHouseID,
										strBounceHouseName = TB.strBounceHouseName,
										intEmployeesNeededForSetup = TB.intEmployeesNeededForSetup,
										intNumberOfBlowersRequired = TB.intNumberOfBlowersRequired,
										intNumberOfStakesRequired = TB.intNumberOfStakesRequired
									};

			return Ok(eventBounceHouses);
		}

		[HttpPost]
		[Route("{id:}")]
		public async Task<IActionResult> GetUnavailableBouncehouses([FromRoute] string id, EventDates dateParams)
		{
			//Check for the event...
			var intEventID = Int32.Parse(id);
			var dbevent = await _jhmsDbContext.TEvents.FirstOrDefaultAsync(x => x.intEventID == intEventID);

			//Store event dates for the query
			string strStartDate = dateParams.strStartDate;
			string strEndDate = dateParams.strEndDate;

			//Parse String dates to DateTime
			DateTime dteStartDate = DateTime.Parse(strStartDate);
			DateTime dteEndDate = DateTime.Parse(strEndDate);

			if (dbevent == null)
			{
				return NotFound();
			}

			//LINQ join
			var bounceHouse = from TBH in _jhmsDbContext.TBounceHouses
								from TEBH in _jhmsDbContext.TEventBounceHouses
								from TE in _jhmsDbContext.TEvents
								where TBH.intBounceHouseID == TEBH.intBounceHouseID
								where TE.intEventID == TEBH.intEventID
								where TE.intEventID != intEventID
								where TE.dteEventStartDate == dteStartDate
							  select new
								{
									bhID = TEBH.intBounceHouseID
								};
			

			return Ok(bounceHouse);
		}

	}
}
