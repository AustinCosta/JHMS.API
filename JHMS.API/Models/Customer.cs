using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models 
{
	public class Customer 
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intCustomerID { get; set; }
		public string strFirstName { get; set; }
		public string strLastName { get; set; }
		public string strAddress { get; set; }
		public string strCity { get; set; }
		public string strState { get; set; }
		public string strZip { get; set; }
		public string strEmail { get; set; }
	}
}
