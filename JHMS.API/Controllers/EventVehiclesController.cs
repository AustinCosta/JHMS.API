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
									intEventVehicleID = TEV.intEventVehicleID,
									intVehicleID = TV.intVehicleID,
									strVehicleName = TV.strVehicleName,
								};

			return Ok(eventVehicles);
		}


		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEventVehicle([FromRoute] string id)
		{
			var intEventVehicleID = Int32.Parse(id);
			var eventVehicle = await _jhmsDbContext.TEventVehicles.FindAsync(intEventVehicleID);

			if (eventVehicle == null)
			{
				return NotFound();
			}

			_jhmsDbContext.TEventVehicles.Remove(eventVehicle);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(eventVehicle);
		}

		//Get Available Vehicles
		[HttpGet]
		[Route("{id:}/{strStartDate:}/{strEndDate:}")]
		public async Task<IActionResult> GetAvailableVehicles([FromRoute] string id, [FromRoute] string strStartDate, [FromRoute] string strEndDate)
		{
			//Get the Event ID
			var intEventID = Int32.Parse(id);
			var dbevent = await _jhmsDbContext.TEvents.FirstOrDefaultAsync(x => x.intEventID == intEventID);

			//Parse dates from string/url
			DateTime dteEventStartDate = DateTime.Parse(strStartDate);
			DateTime dteEventEndDate = DateTime.Parse(strEndDate);

			//Check that event exists
			if (dbevent == null)
			{
				return NotFound();
			}

			var allVehicles = from TV in _jhmsDbContext.TVehicles select new { intVehicleID = TV.intVehicleID, strVehicleName = TV.strVehicleName };

			//Get vehicles for the event ID passed in
			var eventVehicles = from TE in _jhmsDbContext.TEvents
								from TEV in _jhmsDbContext.TEventVehicles
								from TV in _jhmsDbContext.TVehicles
								where TE.intEventID == TEV.intEventID
								where TV.intVehicleID == TEV.intVehicleID
								where TE.dteEventStartDate >= dteEventStartDate && TE.dteEventEndDate <= dteEventEndDate
								select new
								{
									intVehicleID = TV.intVehicleID,
									strVehicleName = TV.strVehicleName,
								};

			var available = allVehicles.Except(eventVehicles);

			return Ok(available);
		}


		[HttpPost]
		public async Task<IActionResult> AddVehicle([FromBody] EventVehicle addVehicleRequest)
		{
			await _jhmsDbContext.TEventVehicles.AddAsync(addVehicleRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(addVehicleRequest);
		}

	}
}
