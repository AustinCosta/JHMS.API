using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventEnvironmentTypesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventEnvironmentTypesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		//Get environment types
		[HttpGet]
		public async Task<IActionResult> GetEnvironmentTypes()
		{
			var environmentTypes = await _jhmsDbContext.TEnvironmentTypes.ToListAsync();

			return Ok(environmentTypes);
		}


		//Get Event Environments
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetAllEventEnvironmentTypes([FromRoute] string id)
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
			var eventEnvironments = from TE in _jhmsDbContext.TEvents
									from TET in _jhmsDbContext.TEnvironmentTypes
									from TEE in _jhmsDbContext.TEventEnvironmentTypes
									where TE.intEventID == TEE.intEventID
									where TET.intEnvironmentTypeID == TEE.intEnvironmentTypeID
									where TE.intEventID == intEventID
									select new
									{
										intEventEnvironmentTypeID = TEE.intEventEnvironmentTypeID,
										intEnvironmentTypeID = TET.intEnvironmentTypeID,
										strEnvironmentType = TET.strEnvironmentType,
									};

			return Ok(eventEnvironments);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEventEnvironmentType([FromRoute] string id)
		{
			var intEventEnvironmentTypeID = Int32.Parse(id);
			var environmentType = await _jhmsDbContext.TEventEnvironmentTypes.FindAsync(intEventEnvironmentTypeID);

			if (environmentType == null)
			{
				return NotFound();
			}

			_jhmsDbContext.TEventEnvironmentTypes.Remove(environmentType);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(environmentType);
		}

		[HttpPost]
		public async Task<IActionResult> AddEventEnvironment([FromBody] EventEnvironmentType eventEnvironmentType)
		{
			await _jhmsDbContext.TEventEnvironmentTypes.AddAsync(eventEnvironmentType);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(eventEnvironmentType);
		}

	}
}
