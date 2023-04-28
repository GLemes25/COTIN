using Common.UnitOfWork;
using Common.UnitOfWork.Collections;
using Northwind.API.Logic.Interfaces;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Services
{
    public class ProdutcsServices : IProductsServices
    {
        private readonly ILogger<ProdutcsServices> _logger;
        private readonly IUnitOfWork<NorthWindContext> _unitOfWork;

        public ProdutcsServices(ILogger<ProdutcsServices> logger, IUnitOfWork<NorthWindContext> unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }
        public async Task<bool> DELETE_Products(int productID)
        {
            try 
            {
                _unitOfWork.GetRepository<Products>().Delete(productID);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<IList<Products>> GET_ListaProducts()
        {
            try 
            {
                return await _unitOfWork.GetRepository<Products>().GetAllAsync();
            }
            catch (Exception) 
            {
                throw;
            };
        }

        public async Task<Products> GET_Products(int productID)
        {
            try 
            {
                return await _unitOfWork.GetRepository<Products>().GetFirstOrDefaultAsync(predicate: x => x.ProductID == productID);
            }
            catch (Exception) 
            {
                throw;
            };
        }

        public async Task<IPagedList<Products>> GET_ProductsPage(int? pageNo = 1, int? pageSize = 20)
        {
            try 
            {
                return await _unitOfWork.GetRepository<Products>().GetPagedListAsync(pageIndex: pageNo.Value, pageSize: pageSize.Value);
            }
            catch (Exception) 
            {
                throw;
            }
        }

        public async Task<bool> POST_Products(Products products)
        {
            try
            {
                await _unitOfWork.GetRepository<Products>().InsertAsync(products);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PUT_Products(Products products)
        {
            try
            {
                _unitOfWork.GetRepository<Products>().Update(products);
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
