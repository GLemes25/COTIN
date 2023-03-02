using System.Collections.Generic;

namespace ConsoleApp2.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Insert(T obj);
        void Delete(T obj);
        T Update(T obj);
        void Save();
    }
}
