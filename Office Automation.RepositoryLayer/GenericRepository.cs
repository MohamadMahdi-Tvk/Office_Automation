using Office_Automation.ModelLayer;
using Office_Automation.ModelLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office_Automation.RepositoryLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        OfficeAutomationContext db;
        DbSet<T> dbContext;

        public GenericRepository(OfficeAutomationContext context)
        {
            db = context;
            dbContext = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.ToList();
        }

        public T GetEntity(int id)
        {
            return dbContext.Find(id);
        }


        public bool Add(T entity)
        {
            try
            {
                dbContext.Add(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(T entity)
        {
            try
            {
                db.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                T entity = GetEntity(id);
                db.Entry(entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
