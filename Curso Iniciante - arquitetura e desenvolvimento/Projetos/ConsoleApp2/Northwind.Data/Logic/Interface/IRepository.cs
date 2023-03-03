using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.Logic.Interface
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> ObterTodos();
        T ObterPorID(int id);
        T Inserir(T obj);
        void Deletar(T obj);
        T Alterar(T obj);
        void Salvar();
    }
}
