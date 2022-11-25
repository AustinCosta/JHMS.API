using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class EventVehicle
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventVehicleID { get; set; }

		[ForeignKey("Event")] //Tells EF that this is a FK to another Entity
		public int intEventID { get; set; }

		[ForeignKey("Vehicle")] //Tells EF that this is a FK to another Entity
		public int intVehicleID { get; set; }
	}
}
