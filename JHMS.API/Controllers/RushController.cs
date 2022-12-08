using JHMS.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class RushController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public RushController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
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

			var allInflatables = from TB in _jhmsDbContext.TBounceHouses
								 where TB.intBounceHouseTypeID == 4
								 select new
								 {
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

		//Get Event Equipments
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetEventRushes([FromRoute] string id)
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
									where TE.intEventID == intEventID && TB.intBounceHouseTypeID == 4
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
