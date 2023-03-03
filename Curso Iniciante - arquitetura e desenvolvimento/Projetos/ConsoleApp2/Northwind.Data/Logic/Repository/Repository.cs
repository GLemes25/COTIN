using Northwind.Data.Logic.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Northwind.Data.Logic.Interface

{
    public class Repository<T> : IRepository<T> where T : class
    { 
        private Data.NorthwindEntities context;

        private DbSet<T> dbSet;

        public Repository()
        {
            context = new NorthwindEntities();
            dbSet = context.Set<T>();
        }

        public IEnumerable<T> ObterTodos()
        {
            return dbSet.ToList();

        }

        public T Alterar(T obj)
        {
            dbSet.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            Salvar();
            return obj;

        }

        public void Deletar(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public T Inserir(T obj)
        {
            dbSet.Add(obj);
            Salvar();
            return obj;

        }

        public T ObterPorID(int id)
        {
            return dbSet.Find(id);

        }



        public void Salvar()
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
        protected virtual void dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}