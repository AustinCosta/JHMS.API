using JHMS.API.Data;
using JHMS.API.Models;
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
										intEventEquipmentID = TEE.intEventEquipmentID,
										intEquipmentID = TEq.intEquipmentID,
										strEquipmentName = TEq.strEquipmentName,
										strDescription = TEq.strDescription,
									};

			return Ok(eventEquipments);
		}


		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEventEquipment([FromRoute] string id)
		{
			var intEventEquipmentID = Int32.Parse(id);
			var eventEquipment = await _jhmsDbContext.TEventEquipments.FindAsync(intEventEquipmentID);

			if (eventEquipment == null)
			{
				return NotFound();
			}

			_jhmsDbContext.TEventEquipments.Remove(eventEquipment);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(eventEquipment);
		}


		[HttpPost]
		public async Task<IActionResult> AddEquipment([FromBody] EventEquipment addEquipmentRequest)
		{
			await _jhmsDbContext.TEventEquipments.AddAsync(addEquipmentRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(addEquipmentRequest);
		}

	}
}
