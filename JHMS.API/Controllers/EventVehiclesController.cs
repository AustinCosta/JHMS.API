using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventVehiclesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventVehiclesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}


		//Get Event Vehicles
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetAllEventVehicles([FromRoute] string id)
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
			var eventVehicles = from TE in _jhmsDbContext.TEvents
								from TEV in _jhmsDbContext.TEventVehicles
								from TV in _jhmsDbContext.TVehicles
								where TE.intEventID == TEV.intEventID
								where TV.intVehicleID == TEV.intVehicleID
								where TE.intEventID == intEventID
								select new
								{
									intVehicleID = TV.intVehicleID,
									strVehicleName = TV.strVehicleName,
								};

			return Ok(eventVehicles);
		}


	}
}
