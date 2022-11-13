using JHMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Data
{
	public class JHMSDbContext : DbContext
	{
		public JHMSDbContext(DbContextOptions options) : base(options)
		{

		}

		//Used to access the Customers table in Sql Server
		public DbSet<Customer> TCustomers { get; set; }
	}
}
