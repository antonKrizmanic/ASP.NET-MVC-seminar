using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Model;

namespace Seminar.DAL.Repository
{
    public class EmployeeRepository:RepositoryBase<Employee>
    {
        public EmployeeRepository(TravelWarrantManagerDbContext context)
            : base(context)
        {
        }

        public override List<Employee> GetList(string username)
        {
            var employeeQuery = this.DbContext.Employees.Include(p => p.Company).AsQueryable();
            if (!string.IsNullOrEmpty(username))
            {
                employeeQuery = employeeQuery.Where(p => p.CreatedByUsername == username);
            }

            return employeeQuery.OrderBy(p => p.ID).ToList(); 
        }
        public List<Employee> GetByCompany(int companyId)
        {
            var employeeQuery =
                this.DbContext.Employees.Include(p => p.Company)
                    .Where(p => p.CompanyID == companyId)
                    .OrderBy(p => p.ID)
                    .ToList();
            return employeeQuery;
        }

        public int GetTravelWarrantsNumber(int id)
        {
            var travelQuery = this.DbContext.TravelWarrants.Include(p => p.Employee).Count(p => p.EmployeeID == id);
            return travelQuery;
        }
    }
}
