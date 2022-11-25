using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class Employee
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEmployeeID { get; set; }

		public string strFirstName { get; set; }

		public string strLastName { get; set; }

		public string strPhoneNumber { get; set; }

		public string strAddress { get; set; }

		public string strCity { get; set; }

		public string strState { get; set; }

		public string strZip { get; set; }

		public string strDateOfBirth { get; set; }

		public string strEmail { get; set; }

		public string strDrivingStatus { get; set; }

		public string strEmploymentTitle { get; set; }
	}
}
