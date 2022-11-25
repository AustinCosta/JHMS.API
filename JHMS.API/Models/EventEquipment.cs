using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class EventEquipment
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventEquipmentID { get; set; }

		[ForeignKey("Event")] //Tells EF that this is a FK to another Entity
		public int intEventID { get; set; }

		[ForeignKey("Equipment")] //Tells EF that this is a FK to another Entity
		public int intEquipmentID { get; set; }
	}
}
