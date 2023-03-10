
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private NorthwindEntities context;

        private DbSet<T> dbSet;

        public Repository()
        {
            context = new NorthwindEntities();
            dbSet = context.Set<T>();
        }

        public void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetByID(int Id)
        {
            return dbSet.Find(Id);

        }

        public T Insert(T obj)
        {
            dbSet.Add(obj);
            Save();
            return obj;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public T Update(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Save();
            return obj;
        }
    }
}
