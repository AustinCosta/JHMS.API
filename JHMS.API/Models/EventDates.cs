using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace JHMS.API.Models
{
	public class EventDates
	{
		public string strStartDate { get; set; }
		public string strEndDate { get; set; }
	}
}
