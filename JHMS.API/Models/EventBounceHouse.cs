using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHMS.API.Models
{
	public class EventBounceHouse
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventBounceHouseID { get; set; }

		[ForeignKey("Event")] //Tells EF that this is a FK to a Event Entity
		public int intEventID { get; set; }

		[ForeignKey("BounceHouse")] //Tells EF that this is a FK to a Event Entity
		public int intBounceHouseID { get; set; }
	}
}
