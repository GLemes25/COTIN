using Newtonsoft.Json;
using NovoConsole.Logic.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace NovoConsole.Logic.Services
{
    public class CategoriesService
    {
        private const string localhost = "https://localhost:44386";
        public List<CategoriesDto> GetCategoriesList()
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/Categories", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<List<CategoriesDto>>(response.Content);
            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public CategoriesDto GetCategoryByID(int categoryID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Categories/{categoryID}", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<CategoriesDto>(response.Content);

            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public CategoriesDto UpdateCategory(int categoryID, CategoriesDto categoriesDto)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Categories/{categoryID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = categoriesDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return categoriesDto;
        }

        public CategoriesDto AddCategory(CategoriesDto categoriesDto)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/Categories", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = categoriesDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return categoriesDto;
        }

        public Boolean DeleteCategoryID(int categoryID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Categories/{categoryID}", Method.Delete);
            RestResponse response = client.Execute(request);

            return true;
        }

    }
}