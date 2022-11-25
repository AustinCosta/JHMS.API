using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class Vehicle
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intVehicleID { get; set; }

		public string strVehicleName { get; set; }

		public int intQuantity { get; set; }
	}
}
