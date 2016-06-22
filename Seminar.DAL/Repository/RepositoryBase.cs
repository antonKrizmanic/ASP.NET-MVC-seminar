using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seminar.Model;

namespace Seminar.DAL.Repository
{
    public abstract class RepositoryBase<TEntity>
        where TEntity : EntityBase
    {
        protected TravelWarrantManagerDbContext DbContext { get; set; }

        public RepositoryBase(TravelWarrantManagerDbContext context)
        {
            this.DbContext = context;
        }

        public virtual List<TEntity> GetList(string username)
        {
            var query = this.DbContext.Set<TEntity>().AsQueryable();
            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(p => p.CreatedByUsername == username);
            }
            return query.OrderBy(p => p.ID).ToList(); 
        } 
        public virtual TEntity Find(int id)
        {
            return this.DbContext.Set<TEntity>().FirstOrDefault(p => p.ID == id);
        }

        public void Add(TEntity model, string loginUsername)
        {
            model.DateCreated=DateTime.Now;
            model.CreatedByUsername = loginUsername;
            this.DbContext.Set<TEntity>().Add(model);
            this.Save();
        }

        public void Update(TEntity model, string loginUsername)
        {
            model.DateModified=DateTime.Now;
            model.UpdatedByUsername = loginUsername;
            this.DbContext.Entry(model).State=EntityState.Modified;
            this.Save();
        }

        public void Delete(int id)
        {
            var entity = this.DbContext.Set<TEntity>().Find(id);
            this.DbContext.Entry(entity).State=EntityState.Deleted;
            this.Save();
        }

        public void Save()
        {
            this.DbContext.SaveChanges();
        }
    }
}
