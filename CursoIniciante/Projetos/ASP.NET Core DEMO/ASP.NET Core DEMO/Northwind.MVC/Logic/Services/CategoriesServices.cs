using Common.Api.Logic.Services;
using Common.UnitOfWork;
using Newtonsoft.Json;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Logic.Interfaces;
using Northwind.MVC.Models;
using RestSharp;
using System.Net;

namespace Northwind.MVC.Logic.Services
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ILogger<CategoriesServices> _logger;
        private IConfiguration _configuration { get; }

        public CategoriesServices(ILogger<CategoriesServices> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<CategoriesViewModel> GET_ListaCategories()
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Get);
                request.AddHeader("cache-control", "no-cache");

                string url = string.Empty;
                url = urlApiNorthwind + $"Categories?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new CategoriesViewModel();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<CategoriesViewModel>(response.Content.ToString());
                }

                return resultado;

            }
            catch (Exception)
            {
                return new CategoriesViewModel();
            };
        }

    }
}
