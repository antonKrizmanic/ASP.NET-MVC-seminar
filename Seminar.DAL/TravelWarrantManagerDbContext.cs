using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Seminar.Model;

namespace Seminar.DAL
{
    public class TravelWarrantManagerDbContext:IdentityDbContext<User>
    {
        public TravelWarrantManagerDbContext():base("TravelWarrantManagerDbContext")
        {
            
        }
        public DbSet<Car> Cars { get; set; } 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TravelWarrant> TravelWarrants { get; set; }
        public static TravelWarrantManagerDbContext Create()
        {
            return new TravelWarrantManagerDbContext();
        }
    }
}
