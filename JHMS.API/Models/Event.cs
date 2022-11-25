using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHMS.API.Models
{
	public class Event
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEventID { get; set; }

		public string strEventType { get; set; }
		
		[ForeignKey("Customer")] //Tells EF that this is a FK to another Entity
		public int intCustomerID { get; set; }

		[ForeignKey("EnvironmentType")]
		public int intEnvironmentTypeID { get; set; }

		[Column(TypeName = "Date")]
		public DateTime dteEventStartDate { get; set; }

		[Column(TypeName = "Date")]
		public DateTime dteEventEndDate { get; set; }

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
