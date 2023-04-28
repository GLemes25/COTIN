using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Services;
using Common.UnitOfWork;
using Common.UnitOfWork.Collections;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Validations;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.Logic.Services
{
    public class CategoriesServices : BaseNotification, ICategoriesServices 
    {
        private readonly ILogger<CategoriesServices> _logger;
        private readonly IUnitOfWork<NorthWindContext> _unitOfWork;
        public CategoriesServices(ILogger<CategoriesServices> logger, IUnitOfWork<NorthWindContext> unitOfWork, INotification notificador) : base(notificador)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Categories> GET_Categories(int categoryID) //task é igual a thread, ela espera a execução.
        {
            try
            {
                return await _unitOfWork.GetRepository<Categories>().GetFirstOrDefaultAsync(predicate: x => x.CategoryID == categoryID);
            }
            catch (Exception)
            {
                throw;
            };
        }

        public async Task<IList<Categories>> GET_ListaCategories()
        {
            try
            {
                return await _unitOfWork.GetRepository<Categories>().GetAllAsync();
            }
            catch (Exception)
            {
                throw;
            };
        }
        public async Task<IPagedList<Categories>> GET_CategoriesPage(int? pageNo = 1, int? pageSize = 20)
        {
            try
            {
                return await _unitOfWork.GetRepository<Categories>().GetPagedListAsync(pageIndex: pageNo.Value, pageSize: pageSize.Value);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> PUT_Categories(Categories categories)
        {
            try
            {
                if (!ExecutValidation(new CategoriesValidation(), categories)) return false;

                _unitOfWork.GetRepository<Categories>().Update(categories);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> POST_Categories(Categories categories)
        {
            try
            {
                if (!ExecutValidation(new CategoriesValidation(), categories)) return false;

                await _unitOfWork.GetRepository<Categories>().InsertAsync(categories);
                await _unitOfWork.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DELETE_Categories(int categoryID)
        {
            try
            {
                _unitOfWork.GetRepository<Categories>().Delete(categoryID);
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

