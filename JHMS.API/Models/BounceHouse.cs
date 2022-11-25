using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class BounceHouse
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intBounceHouseID { get; set; }

		[ForeignKey("BounceHouseType")] //Tells EF that this is a FK to a Customer Entity
		public int intBounceHouseTypeID { get; set; }

		public string strBounceHouseName { get; set; }

		public string strDescription { get; set; }

		public int intEmployeesNeededForSetup { get; set; }

		public int intNumberOfStakesRequired { get; set; }

		public int intNumberOfBlowersRequired { get; set; }

		public string strDateOfLastPurchase { get; set; }

		public string strURL { get; set; }

		public int intPurchaseYear { get; set; }
	}
}
