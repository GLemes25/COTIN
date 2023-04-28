using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace NovoConsole.Logic.Services
{
    public class OrdersService
    {
        private const string localhost = "https://localhost:44386";
        public List<OrderDto> GetOrdersList()
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest("/api/Orders", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<List<OrderDto>>(response.Content);
            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public OrderDto GetOrdersByID(int orderID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Orders/{orderID}", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<OrderDto>(response.Content);

            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public List<OrderDto> GetOrdersByName(string orderName)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            string urlEncodedName = Uri.EscapeDataString(orderName);
            var request = new RestRequest($"/api/Orders/GetByName/{urlEncodedName}", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<List<OrderDto>>(response.Content);
            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
        public OrderDto UpdateOrders(int orderID, OrderDto ordersDto)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Orders/{orderID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            var body = ordersDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return ordersDto;
        }
        //---------------------------------------------------------------------------------------------------


        public OrderDto AddOrders(OrderDto ordersDto)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/Orders", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = ordersDto;
            request.AddStringBody(JsonConvert.SerializeObject(body), DataFormat.Json);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            return ordersDto;
        }

        //---------------------------------------------------------------------------------------------------


        public Boolean DeleteOrdersID(int orderID)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Orders/{orderID}", Method.Delete);
            RestResponse response = client.Execute(request);

            return true;
        }

        //---------------------------------------------------------------------------------------------------

        public List<OrderDto> GetOrdersListByPage(int pageNo = 1, int pageSize = 20)
        {
            var options = new RestClientOptions(localhost)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest($"/api/Orders/GetByPage?pageNo={pageNo}&pageSize={pageSize}", Method.Get);
            RestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);
            var lstresponse = JsonConvert.DeserializeObject<List<OrderDto>>(response.Content);
            return lstresponse;
        }
        //---------------------------------------------------------------------------------------------------
    }
}