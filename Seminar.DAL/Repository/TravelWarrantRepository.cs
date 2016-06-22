using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Model;

namespace Seminar.DAL.Repository
{
    public class TravelWarrantRepository:RepositoryBase<TravelWarrant>
    {
        public TravelWarrantRepository(TravelWarrantManagerDbContext context) : base(context)
        {
        }

        public override List<TravelWarrant> GetList(string username)
        {
            var travelWarrantQuery = this.DbContext.TravelWarrants.Include(p => p.Car.Company.Employees).AsQueryable();
            if (!string.IsNullOrEmpty(username))
            {
                travelWarrantQuery = travelWarrantQuery.Where(p => p.CreatedByUsername == username);
            }
            return travelWarrantQuery.OrderBy(p => p.ID).ToList(); 
        }
        public new void Add(TravelWarrant model, string loginUsername)
        {
            model.DateCreated = DateTime.Now;
            model.CreatedByUsername = loginUsername;
            model.Kilometer = model.EndKilometer - model.StartKilometer;
            this.DbContext.TravelWarrants.Add(model);
            this.Save();
        }
        public new void Update(TravelWarrant model, string loginUsername)
        {
            model.DateModified = DateTime.Now;
            model.UpdatedByUsername = loginUsername;
            model.Kilometer = model.EndKilometer - model.StartKilometer;
            this.DbContext.Entry(model).State = EntityState.Modified;
            this.Save();
        }
        public List<TravelWarrant> FindWarrants(int id,int month)
        {
            var travelQuery = this.DbContext.TravelWarrants.Include(p => p.Company).Where(p => p.CompanyID== id && p.Date.Month==month).ToList();
            return travelQuery;
        }
    }
}
