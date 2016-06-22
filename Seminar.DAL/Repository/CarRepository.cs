using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Seminar.Model;

namespace Seminar.DAL.Repository
{
    public class CarRepository:RepositoryBase<Car>
    {
        public CarRepository(TravelWarrantManagerDbContext context) : base(context)
        {
        }

        public override List<Car> GetList(string username)
        {
            var carQuery = this.DbContext.Cars.Include(p => p.Company).AsQueryable();
            if (!string.IsNullOrEmpty(username))
            {
                carQuery = carQuery.Where(p => p.CreatedByUsername == username);
            }
            return carQuery.OrderBy(p => p.ID).ToList();
        }

        public List<Car> GetByCompany(int companyId)
        {
            var carQuery =
                this.DbContext.Cars.Include(p => p.Company)
                    .Where(p => p.CompanyID == companyId)
                    .OrderBy(p => p.ID)
                    .ToList();
            return carQuery;
        } 
        public List<TravelWarrant> GetTravelWarrants(int id)
        {
            var travelQuery = this.DbContext.TravelWarrants.Include(p => p.Car).Where(p => p.CarID == id).ToList();
            return travelQuery;
        }
        public int GetTravelWarrantsNumber(int id)
        {
            var travelQuery = this.DbContext.TravelWarrants.Include(p => p.Car).Count(p => p.CarID == id);
            return travelQuery;
        }

    }
}
