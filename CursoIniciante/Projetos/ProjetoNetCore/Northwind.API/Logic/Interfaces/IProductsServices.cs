using Common.UnitOfWork.Collections;
using NorthWind.Data.Logic.Data.NorthWind.Entity;

namespace Northwind.API.Logic.Interfaces
{
    public interface IProductsServices
    {
        Task<Products> GET_Products(int productID);
        Task<IList<Products>> GET_ListaProducts();
        Task<IPagedList<Products>> GET_ProductsPage(int? pageNo = 1, int? pageSize = 20); //Lista páginada
        Task<bool> PUT_Products(Products products);
        Task<bool> POST_Products(Products products);
        Task<bool> DELETE_Products(int productID);
    }
}
