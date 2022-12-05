using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class frntEvent
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventID { get; set; }

		public string strEventType { get; set; }

		[ForeignKey("Customer")] //Tells EF that this is a FK to another Entity
		public int intCustomerID { get; set; }

		[ForeignKey("EnvironmentType")]
		public int intEnvironmentTypeID { get; set; }

		public string strEventStartDate { get; set; }

		public string strEventEndDate { get; set; }

		public string strEventName { get; set; }

		public string strEventStartTime { get; set; }

		public string strEventEndTime { get; set; }

		public string strEventSetupTime { get; set; }

		public string strEventDescription { get; set; }

		public int intInflatablesNeeded { get; set; }

		public int intEmployeesForTheEvent { get; set; }

		public string strLocation { get; set; }
	}
}
