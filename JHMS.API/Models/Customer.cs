using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models 
{
	public class Customer 
	{
		[Key]
		public int intCustomerID { get; set; }
		public string strCustomerName { get; set; }

	}
}
