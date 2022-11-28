using JHMS.API.Data;
using JHMS.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Controllers
{
	[ApiController] //Tell .Net this is an API controller --- NO VIEWS
	[Route("/api/[controller]")]
	public class EquipmentsController : Controller
	{
		private readonly JHMSDbContext _jhmsDbContext;

		public EquipmentsController(JHMSDbContext jhmsDbContext)
		{
			_jhmsDbContext = jhmsDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllEquipments ()
		{
			//Get a list of all customers from TCustomers
			var equipment = await _jhmsDbContext.TEquipments.ToListAsync();

			//Return OK Response with customers data
			return Ok(equipment);
		}

		[HttpPost]
		public async Task<IActionResult> AddEquipment([FromBody] Equipment equipmentRequest)
		{
			await _jhmsDbContext.TEquipments.AddAsync(equipmentRequest);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(equipmentRequest);
		}

		[HttpGet]
		[Route("{id:}")]
		public async Task<IActionResult> GetEquipment([FromRoute] string id)
		{
			var intEquipmentID = Int32.Parse(id);
			var equipment = await _jhmsDbContext.TEquipments.FirstOrDefaultAsync(x => x.intEquipmentID == intEquipmentID);

			if (equipment == null)
			{
				return NotFound();
			}

			return Ok(equipment);
		}

		[HttpPut]
		[Route("{id:}")]
		public async Task<IActionResult> UpdateEquipment([FromRoute] string id, Equipment updateEquipmentRequest)
		{
			var intEquipmentID = Int32.Parse(id);
			var equipment = await _jhmsDbContext.TEquipments.FindAsync(intEquipmentID);

			if (equipment == null)
			{
				NotFound();
			}

			equipment.strEquipmentName = updateEquipmentRequest.strEquipmentName;
			equipment.strDescription = updateEquipmentRequest.strDescription;
			equipment.intQuantityOnHand = updateEquipmentRequest.intQuantityOnHand;
			equipment.intExpectedQuantity = updateEquipmentRequest.intExpectedQuantity;

			await _jhmsDbContext.SaveChangesAsync();

			return Ok(equipment);
		}

		[HttpDelete]
		[Route("{id:}")]
		public async Task<IActionResult> DeleteEquipment([FromRoute] string id)
		{
			var intEquipmentID = Int32.Parse(id);
			var equipment = await _jhmsDbContext.TEquipments.FindAsync(intEquipmentID);

			if (equipment == null)
			{
				return NotFound();
			}

			//Delete from EventEquipments first
			await _jhmsDbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM TEventEquipments WHERE intEquipmentID = {intEquipmentID}"); 
			await _jhmsDbContext.SaveChangesAsync();

			_jhmsDbContext.TEquipments.Remove(equipment);
			await _jhmsDbContext.SaveChangesAsync();

			return Ok(equipment);
		}
	}
}
