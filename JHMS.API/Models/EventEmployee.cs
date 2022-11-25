using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class EventEmployee
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventEmployeeID { get; set; }

		[ForeignKey("Event")] //Tells EF that this is a FK to a Event Entity
		public int intEventID { get; set; }

		[ForeignKey("Employee")] //Tells EF that this is a FK to a Event Entity
		public int intEmployeeID { get; set; }
	}
}
