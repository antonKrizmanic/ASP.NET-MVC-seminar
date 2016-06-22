using Microsoft.AspNet.Identity.EntityFramework;
using Seminar.Model;

namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Seminar.DAL.TravelWarrantManagerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TravelWarrantManagerDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Companies.AddOrUpdate(p=>p.ID,
                new Company() {ID=1, Name="Test1",Address = "Vrbik", Email = "nesto@nesto.com",OIB="98810845644",City="Umag", DateCreated = DateTime.Now},
                new Company() {ID=2, Name="tesx1",Address = "Kon 2", Email = "nesto@nesto.com", OIB = "12345678912",City="Buje", DateCreated = DateTime.Now});
            context.Roles.AddOrUpdate(p => p.Id,
                new IdentityRole() { Id = "1", Name = "Admin" },
                new IdentityRole() { Id = "2", Name = "Manager" });
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
