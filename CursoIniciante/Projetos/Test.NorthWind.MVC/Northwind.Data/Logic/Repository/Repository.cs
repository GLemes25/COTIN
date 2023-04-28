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
        private NorthwindEntities contexto;

        private DbSet<T> DbSet;

        public Repository()
        {
            contexto = new NorthwindEntities();

            DbSet = contexto.Set<T>();
        }
        public T Update(T obj)
        {
            DbSet.Attach(obj);
            contexto.Entry(obj).State = EntityState.Modified;
            Save();
            return obj;
        }

        public void Delete(object Id)
        {
            T entityToDelete = DbSet.Find(Id);
            Delete(entityToDelete);
        }

        public void Delete(T entityToDelete)
        {
            if (contexto.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public T Insert(T obj)
        {
            DbSet.Add(obj);
            Save();
            return obj;
        }

        public T GetByID(object Id)
        {
            return DbSet.Find(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public void Save()
        {
            try
            {
                contexto.SaveChanges();
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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (contexto != null)
                {
                    contexto.Dispose();
                    contexto = null;
                }
            }
        }
    }
}
