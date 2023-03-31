using Newtonsoft.Json;
using NovoConsole.Logic.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NovoConsole.Logic.Services
{
    public class SuppliersService
    {
        private const string localhost = "https://localhost:44386";
        public List<SupplierDto> ObterListaSupplier()
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/Suppliers", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<List<SupplierDto>>(response.Content);
            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public SupplierDto ObterSupplierID(int supplierID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Suppliers/{supplierID}", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<SupplierDto>(response.Content);

            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public SupplierDto AlterarSupplier(int supplierID, SupplierDto supplierDto)
        {
            var options = new RestClientOptions("https://localhost:44386/")
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Suppliers/{supplierID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = supplierDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return supplierDto;
        }

        public SupplierDto IncluirSupplier(SupplierDto supplierDto)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/Suppliers", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = supplierDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return supplierDto;
        }

        public Boolean DeteleteSupplierID(int supplierID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Suppliers/{supplierID}", Method.Delete);
            RestResponse response = client.Execute(request);

            return true;
        }

    }
}