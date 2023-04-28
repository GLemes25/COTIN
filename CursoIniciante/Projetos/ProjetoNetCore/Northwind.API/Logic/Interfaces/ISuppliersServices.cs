using Common.UnitOfWork.Collections;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Interfaces
{
    public interface ISuppliersServices
    {
        Task<Suppliers> GET_Suppliers(int supplierID);
        Task<IList<Suppliers>> GET_ListaSuppliers();
        Task<IPagedList<Suppliers>> GET_SuppliersPage(int? pageNo = 1, int? pageSize = 20); //Lista páginada
        Task<bool> PUT_Suppliers(Suppliers suppliers);
        Task<bool> POST_Suppliers(Suppliers suppliers);
        Task<bool> DELETE_Suppliers(int supplierID);

    }
}
