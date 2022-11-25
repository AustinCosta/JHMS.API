using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class Equipment
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEquipmentID { get; set; }

		public string strEquipmentName { get; set; }

		public string strDescription { get; set; }

		public int intQuantityOnHand { get; set; }

		public int intExpectedQuantity { get; set; }
	}
}
