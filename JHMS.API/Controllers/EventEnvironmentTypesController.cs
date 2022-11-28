using JHMS.API.Data;
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
										intEnvironmentTypeID = TET.intEnvironmentTypeID,
										strEnvironmentType = TET.strEnvironmentType,
									};

			return Ok(eventEnvironments);
		}

	}
}
