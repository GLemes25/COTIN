using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetByID(object Id);
        T Insert(T obj);
        void Delete(object Id);
        T Update(T obj);
        void Save();
    }
}
