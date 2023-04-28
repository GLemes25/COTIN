using Common.UnitOfWork.Collections;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Models;

namespace Northwind.MVC.Logic.Interfaces
{
    public interface ICategoriesServices
    {
        Task<CategoriesViewModel> GET_ListaCategories();
    }
}
