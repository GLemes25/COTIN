using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace NorthWind.Data.Logic.Repository
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
        public T Alterar(T obj)
        {
            DbSet.Attach(obj);
            contexto.Entry(obj).State = EntityState.Modified;
            Salvar();
            return obj;
        }

        public void Apagar(object Id)
        {
            T entityToDelete = DbSet.Find(Id);
            Apagar(entityToDelete);
        }

        public void Apagar(T entityToDelete)
        {
            if (contexto.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }
        public T Inserir(T obj)
        {
            DbSet.Add(obj);
            Salvar();
            return obj;
        }

        public T ObterPorID(object Id)
        {
            return DbSet.Find(Id);
        }

        public IEnumerable<T> ObterTodos()
        {
            return DbSet.ToList();
        }

        public void Salvar()
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

        //public IEnumerable<T> ObterPorPagina(int itemPorPagina, int IndexPagina)
        //{
        //    return DbSet.OrderBy(x => x.Id).Skip(IndexPagina * itemPorPagina).Take(itemPorPagina).ToList();
        //}
    }
}
