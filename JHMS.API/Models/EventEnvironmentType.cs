using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class EventEnvironmentType
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventEnvironmentTypeID { get; set; }

		[ForeignKey("Event")] //Tells EF that this is a FK to another Entity
		public int intEventID { get; set; }

		[ForeignKey("EnvironmentType")] //Tells EF that this is a FK to another Entity
		public int intEnvironmentTypeID { get; set; }
	}
}
