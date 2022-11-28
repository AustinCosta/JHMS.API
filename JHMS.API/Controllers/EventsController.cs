using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHMS.API.Controllers
{
	[ApiController] //Tell .Net this is an API controller --- NO VIEWS
	[Route("/api/[controller]")]
	public class EventsController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventsController(JHMSDbContext jhmsDbContext)
		{
			//inject the db context to be used by the controller
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEvents()
		{
			var events = await _jhmsDbContext.TEvents.ToListAsync();

			return Ok(events);
		}

		[HttpPost]
		public async Task<IActionResult> AddEvent([FromBody] Event eventRequest)
		{
			await _jhmsDbContext.TEvents.AddAsync(eventRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(eventRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetEvent([FromRoute] string id)
		{
			var intEventID = Int32.Parse(id);
			var dbevent = await _jhmsDbContext.TEvents.FirstOrDefaultAsync(x => x.intEventID == intEventID);

			if (dbevent == null)
			{
				return NotFound();
			}

			return Ok(dbevent);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateEvent([FromRoute] string id, Event updateEventRequest)
		{
			var intEventID = Int32.Parse(id);
			var dbevent = await _jhmsDbContext.TEvents.FindAsync(intEventID);

			if (dbevent == null)
			{
				NotFound();
			}

			//Send update values from UI to the dbcontext --- forwards to the database
			dbevent.strEventType = updateEventRequest.strEventType;
			dbevent.intCustomerID = updateEventRequest.intCustomerID;
			dbevent.intEnvironmentTypeID = updateEventRequest.intEnvironmentTypeID;
			dbevent.dteEventStartDate = updateEventRequest.dteEventStartDate;
			dbevent.dteEventEndDate = updateEventRequest.dteEventEndDate;
			dbevent.strEventName = updateEventRequest.strEventName;
			dbevent.strEventStartTime = updateEventRequest.strEventStartTime;
			dbevent.strEventEndTime = updateEventRequest.strEventEndTime;
			dbevent.strEventSetupTime = updateEventRequest.strEventSetupTime;
			dbevent.strEventDescription = updateEventRequest.strEventDescription;
			dbevent.intInflatablesNeeded = updateEventRequest.intInflatablesNeeded;
			dbevent.intEmployeesForTheEvent = updateEventRequest.intEmployeesForTheEvent;
			dbevent.strLocation = updateEventRequest.strLocation;

			await _jhmsDbContext.SaveChangesAsync();

			return Ok(dbevent);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEvent([FromRoute] string id)
		{
			var intEventID = Int32.Parse(id);

			//All tables that have EventID as a FK
			var dbevent = await _jhmsDbContext.TEvents.FindAsync(intEventID);

			//Check for an event with corresponding id
			if (dbevent == null)
			{
				return NotFound();
			}

			//Delete all foreign keys first

			// EventBounceHouses
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventBounceHouses WHERE intEventID = {intEventID}"); //Delete from EventBounceHouses
			await _jhmsDbContext.SaveChangesAsync();

			// EventEmployees
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventEmployees WHERE intEventID = {intEventID}"); //Delete from EventEmployees
			await _jhmsDbContext.SaveChangesAsync();

			// EventEnvironmentTypes
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventEnvironmentTypes WHERE intEventID = {intEventID}"); //Delete from EventEnvironmentTypes
			await _jhmsDbContext.SaveChangesAsync();

			// EventEquipments
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventEquipments WHERE intEventID = {intEventID}"); //Delete from EventEquipments
			await _jhmsDbContext.SaveChangesAsync();

			// EventVehicles
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventVehicles WHERE intEventID = {intEventID}"); //Delete from EventVehicles
			await _jhmsDbContext.SaveChangesAsync();

			//Delete the event
			_jhmsDbContext.TEvents.Remove(dbevent);
			await _jhmsDbContext.SaveChangesAsync();

			//Return the event
			return Ok(dbevent);

		}
	}
}
