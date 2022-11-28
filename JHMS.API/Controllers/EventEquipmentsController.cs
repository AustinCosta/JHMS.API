using JHMS.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController]
	[Route("/api/[controller]")]
	public class EventEquipmentsController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EventEquipmentsController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		//Get Event Equipments
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
			var eventEquipments = from TE in _jhmsDbContext.TEvents
									from TEE in _jhmsDbContext.TEventEquipments
									from TEq in _jhmsDbContext.TEquipments
									where TE.intEventID == TEE.intEventID
									where TEE.intEquipmentID == TEq.intEquipmentID
									where TE.intEventID == intEventID
									select new
									{
										intEquipmentID = TEq.intEquipmentID,
										strEquipmentName = TEq.strEquipmentName,
										strDescription = TEq.strDescription,
									};

			return Ok(eventEquipments);
		}

	}
}
