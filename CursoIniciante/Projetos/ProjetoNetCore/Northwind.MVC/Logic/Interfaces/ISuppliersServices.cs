using Northwind.MVC.Models;

namespace Northwind.MVC.Logic.Interfaces
{
    public interface ISuppliersServices
    {
        Task<SuppliersViewModel> GET_ListaSuppliers();

    }
}
