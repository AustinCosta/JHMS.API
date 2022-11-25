using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class BounceHouseType
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intBounceHouseTypeID { get; set; }

		public string strBounceHouseType { get; set; }

		public string strDescription { get; set; }

	}
}
