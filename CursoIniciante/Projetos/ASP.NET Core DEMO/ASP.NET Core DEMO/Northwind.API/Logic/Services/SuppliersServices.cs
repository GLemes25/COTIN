using Common.UnitOfWork.Collections;
using Common.UnitOfWork;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.API.Logic.Interfaces;

namespace Northwind.API.Logic.Services
{
    public class SuppliersServices : ISuppliersServices
    {
        private readonly ILogger<SuppliersServices> _logger;
        private readonly IUnitOfWork<NorthWindContext> _unitOfWork;

        public SuppliersServices(ILogger<SuppliersServices> logger, IUnitOfWork<NorthWindContext> unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> DELETE_Suppliers(int supplierID)
        {
            try
            {
                _unitOfWork.GetRepository<Suppliers>().Delete(supplierID);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IList<Suppliers>> GET_ListaSuppliers()
        {
            try
            {
                return await _unitOfWork.GetRepository<Suppliers>().GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<Suppliers> GET_Suppliers(int supplierID)
        {
            try
            {
                return await _unitOfWork.GetRepository<Suppliers>().GetFirstOrDefaultAsync(predicate: x => x.SupplierID == supplierID);
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<IPagedList<Suppliers>> GET_SuppliersPage(int? pageNo = 1, int? pageSize = 20)
        {
            try
            {
                return await _unitOfWork.GetRepository<Suppliers>().GetPagedListAsync(pageIndex: pageNo.Value, pageSize: pageSize.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> POST_Suppliers(Suppliers suppliers)
        {
            try
            {
                await _unitOfWork.GetRepository<Suppliers>().InsertAsync(suppliers);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PUT_Suppliers(Suppliers suppliers)
        {
            try
            {
                _unitOfWork.GetRepository<Suppliers>().Update(suppliers);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}

