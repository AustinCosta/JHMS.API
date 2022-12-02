using JHMS.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventEmployeesController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventEmployeesController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		//Get Event Equipments
		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetAllEventEmployees([FromRoute] string id)
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
			var eventEmployees =  from TEE in _jhmsDbContext.TEventEmployees
								  from TE in _jhmsDbContext.TEmployees
								  from TEv in _jhmsDbContext.TEvents
								  where TEE.intEmployeeID == TE.intEmployeeID
								  where TEE.intEventID == TEv.intEventID
								  where TEv.intEventID == intEventID
								  select new
								  {
									  intEventEmployeeID = TEE.intEventEmployeeID,
									  intEmployeeID = TE.intEmployeeID,
									  strFirstName = TE.strFirstName,
									  strLastName = TE.strLastName,
									  strPhoneNumber = TE.strPhoneNumber,
									  strDrivingStatus = TE.strDrivingStatus,
									  strEmploymentTitle = TE.strEmploymentTitle
								  };

			return Ok(eventEmployees);
		}


		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEventEmployee([FromRoute] string id)
		{
			var intEventEmployeeID = Int32.Parse(id);
			var eventEmployee = await _jhmsDbContext.TEventEmployees.FindAsync(intEventEmployeeID);

			if (eventEmployee == null)
			{
				return NotFound();
			}

			_jhmsDbContext.TEventEmployees.Remove(eventEmployee);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(eventEmployee);
		}
	}
}
