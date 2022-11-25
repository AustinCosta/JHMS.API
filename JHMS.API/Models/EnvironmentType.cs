using System.ComponentModel.DataAnnotations;

namespace JHMS.API.Models
{
	public class EnvironmentType
	{
		[Key] //Tells EntityFramework that this is a primary key
		public int intEnvironmentTypeID { get; set; }

		public string strEnvironmentType { get; set; }
	}
}
