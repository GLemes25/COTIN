using System.Collections.Generic;
using System.Data.Entity;

namespace NorthWind.Data.Logic.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> ObterTodos();


        T ObterPorID(object Id);
        T Inserir(T obj);
        void Apagar(object Id);
        T Alterar(T obj);
        void Salvar();
    }
}
