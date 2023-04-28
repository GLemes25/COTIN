using Common.UnitOfWork.Collections;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Models;

namespace Northwind.MVC.Logic.Interfaces
{
    public interface IProductsServices
    {
        Task<Products> GET_Products(int productID);
        Task<ProdutoResponseViewModel> GET_ListaProducts();
        Task<IPagedList<Products>> GET_ProductsPage(int? pageNo = 1, int? pageSize = 20); //Lista páginada
        Task<ProductsResponseViewModel> PUT_Products(ProductsViewModel products);
        Task<ProductsResponseViewModel> POST_Products(ProductsViewModel products);
        Task<bool> DELETE_Products(int productID);
    }
}
