using Common.UnitOfWork.Collections;
using NorthWind.Data.Logic.Data.NorthWind.Entity;

namespace Northwind.API.Logic.Interfaces
{
    public interface ICategoriesServices 
    {
        Task<Categories> GET_Categories(int categoryID);
        Task<IList<Categories>> GET_ListaCategories();
        Task<IPagedList<Categories>> GET_CategoriesPage(int? pageNo = 1, int? pageSize = 20); //Lista páginada
        Task<bool> PUT_Categories(Categories categories);
        Task<bool> POST_Categories(Categories categories);
        Task<bool> DELETE_Categories(int categoryID);
    }
}
