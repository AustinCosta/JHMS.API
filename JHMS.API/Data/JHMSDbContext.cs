using JHMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JHMS.API.Data
{
	public class JHMSDbContext : DbContext
	{
		public JHMSDbContext(DbContextOptions options) : base(options)
		{

		}

		//  =====  dbJHMS TABLES  =====  //

		//THIS ALLOWS US TO MAP EACH TABLE TO THE MODELS WE'VE DEFINED IN THE MODELS FOLDER//

		public DbSet<BounceHouse> TBounceHouses { get; set; }

		public DbSet<BounceHouseType> TBounceHouseTypes { get; set; }

		public DbSet<Customer> TCustomers { get; set; }

		public DbSet<Employee> TEmployees { get; set; }

		public DbSet<EnvironmentType> TEnvironmentTypes { get; set; }

		public DbSet<Equipment> TEquipments { get; set; }

		public DbSet<EventBounceHouse> TEventBounceHouses { get; set; }

		public DbSet<EventEmployee> TEventEmployees { get; set; }

		public DbSet<EventEnvironmentType> TEventEnvironmentTypes { get; set; }

		public DbSet<EventEquipment> TEventEquipments { get; set; }

		public DbSet<Event> TEvents { get; set; }

		public DbSet<EventVehicle> TEventVehicles { get; set; }

		public DbSet<Vehicle> TVehicles { get; set; }

		//  =====  dbJHMS TABLES  =====  //
	}
}
