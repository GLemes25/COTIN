using Newtonsoft.Json;
using Northwind.MVC.Logic.Interfaces;
using Northwind.MVC.Models;
using RestSharp;
using System.Net;

namespace Northwind.MVC.Logic.Services
{
    public class SuppliersServices : ISuppliersServices
    {
        private readonly ILogger<SuppliersServices> _logger;
        private IConfiguration _configuration { get; }

        public SuppliersServices(ILogger<SuppliersServices> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<SuppliersViewModel> GET_ListaSuppliers()
        {
            try
            {
                var urlApiNorthwind = _configuration["AppSettings:APINorthwind"];

                var request = new RestRequest(string.Empty, Method.Get);
                request.AddHeader("cache-control", "no-cache");

                string url = string.Empty;
                url = urlApiNorthwind + $"Suppliers?api-version=1.0";

                var client = new RestClient(url);
                var response = await client.ExecuteAsync(request);

                var resultado = new SuppliersViewModel();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    resultado = JsonConvert.DeserializeObject<SuppliersViewModel>(response.Content.ToString());
                }

                return resultado;

            }
            catch (Exception)
            {
                return new SuppliersViewModel();
            };
        }

    }

}

