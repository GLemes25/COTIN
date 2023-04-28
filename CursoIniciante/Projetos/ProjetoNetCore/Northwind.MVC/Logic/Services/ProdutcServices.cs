using Common.UnitOfWork.Collections;
using Common.UnitOfWork;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Logic.Interfaces;
using Newtonsoft.Json;
using NuGet.Common;
using RestSharp;
using System.Net;
using Northwind.MVC.Models;
using Microsoft.CodeAnalysis;

namespace Northwind.MVC.Logic.Services
{
    public class ProdutcsServices : IProductsServices
    {
        private readonly ILogger<ProdutcsServices> _logger;
        private readonly IUnitOfWork<NorthWindContext> _unitOfWork;
        private IConfiguration _configuration { get; }

        public ProdutcsServices(ILogger<ProdutcsServices> logger, IUnitOfWork<NorthWindContext> unitOfWork, IConfiguration configuration)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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

        public async Task<ProdutoResponseViewModel> GET_ListaProducts()
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Get);
                request.AddHeader("cache-control", "no-cache");

                string url = string.Empty;
                url = urlApiNorthwind + $"Products?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new ProdutoResponseViewModel();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<ProdutoResponseViewModel>(response.Content.ToString());
                }

                return resultado;
            }
            catch (Exception)
            {
                return new ProdutoResponseViewModel();
            };
        }

        public async Task<Products> GET_Products(int productID)
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Get);
                request.AddHeader("cache-control", "no-cache");

                string url = string.Empty;
                url = urlApiNorthwind + $"Products/{productID}?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new Products();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<Products>(response.Content.ToString());
                }

                return resultado;
            }
            catch (Exception)
            {
                return new Products();
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

        public async Task<ProductsResponseViewModel> POST_Products(ProductsViewModel products)
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Post);
                request.AddHeader("cache-control", "no-cache");

                var body = JsonConvert.SerializeObject(products);
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                string url = string.Empty;
                url = urlApiNorthwind + $"Products/InserirProduto?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new ProductsResponseViewModel();


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<ProductsResponseViewModel>(response.Content.ToString());
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductsResponseViewModel> PUT_Products(ProductsViewModel products)
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Put);
                request.AddHeader("cache-control", "no-cache");

                var body = JsonConvert.SerializeObject(products);
                request.AddParameter("application/json", body, ParameterType.RequestBody);

                string url = string.Empty;
                url = urlApiNorthwind + $"Products/AlterarProduto?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new ProductsResponseViewModel();


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<ProductsResponseViewModel>(response.Content.ToString());
                }

                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
