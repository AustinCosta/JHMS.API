using JHMS.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventBH2Controller : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventBH2Controller(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		//Get Event Equipments
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetEventObstacles([FromRoute] string id)
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
									from TBT in _jhmsDbContext.TBounceHouseTypes
									where TE.intEventID == TEB.intEventID
									where TB.intBounceHouseID == TEB.intBounceHouseID
									where TBT.intBounceHouseTypeID == TB.intBounceHouseTypeID
									where TE.intEventID == intEventID && TB.intBounceHouseTypeID == 1
									select new
									{
										intEventBounceHouseID = TEB.intEventBounceHouseID,
										intBounceHouseID = TB.intBounceHouseID,
										strBounceHouseName = TB.strBounceHouseName,
										strBounceHouseType = TB.intBounceHouseTypeID
									};

			return Ok(eventBounceHouses);
		}
	}
}
