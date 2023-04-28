using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using Swashbuckle.Swagger;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class OrdersController : ApiController
    {
        IRepository<Orders> _Repository = new Repository<Orders>();

        // GET: api/Orders
        public IQueryable<OrderDto> GetOrders()
        {
            var orders = _Repository.ObterTodos();
            var ordersDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto();
                orderDto.OrderID = order.OrderID;   
                orderDto.CustomerID = order.CustomerID;   
                orderDto.EmployeeID = order.EmployeeID;   
                orderDto.OrderDate = order.OrderDate;
                orderDto.RequiredDate = order.RequiredDate;
                orderDto.ShippedDate = order.ShippedDate;
                orderDto.ShipVia = order.ShipVia;
                orderDto.Freight = order.Freight;
                orderDto.ShipName = order.ShipName;
                orderDto.ShipAddress = order.ShipAddress;
                orderDto.ShipCity = order.ShipCity;
                orderDto.ShipRegion = order.ShipRegion;
                orderDto.ShipPostalCode = order.ShipPostalCode;
                orderDto.ShipCountry = order.ShipCountry;

                ordersDto.Add(orderDto);
            }
            return ordersDto.AsQueryable();
        }

        // GET: api/Orders
        [ResponseType(typeof(OrderDto))]
        [Route("api/Orders/GetByPage/")]
        public IHttpActionResult GetOrderByPage(int pageNo = 1, int pageSize = 20)
        {
            var orders = _Repository.ObterTodos();

            // Pagina os pedidos usando os métodos Skip() e Take()
            var ordersPaginados = orders.Skip((pageNo - 1) * pageSize).Take(pageSize);

            var ordersDto = new List<OrderDto>();

            foreach (var order in ordersPaginados)
            {
                var orderDto = new OrderDto();
                orderDto.OrderID = order.OrderID;
                orderDto.CustomerID = order.CustomerID;
                orderDto.EmployeeID = order.EmployeeID;
                orderDto.OrderDate = order.OrderDate;
                orderDto.RequiredDate = order.RequiredDate;
                orderDto.ShippedDate = order.ShippedDate;
                orderDto.ShipVia = order.ShipVia;
                orderDto.Freight = order.Freight;
                orderDto.ShipName = order.ShipName;
                orderDto.ShipAddress = order.ShipAddress;
                orderDto.ShipCity = order.ShipCity;
                orderDto.ShipRegion = order.ShipRegion;
                orderDto.ShipPostalCode = order.ShipPostalCode;
                orderDto.ShipCountry = order.ShipCountry;

                ordersDto.Add(orderDto);
            }

            // Retorna a lista paginada de objetos DTO de pedidos
            return Ok(ordersDto);
        }

        // GET: api/Orders/5
        [ResponseType(typeof(OrderDto))]
        public IHttpActionResult GetOrders(string id)
        {
            var order = _Repository.ObterPorID(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderDto();

            orderDto.OrderID = order.OrderID;
            orderDto.CustomerID = order.CustomerID;
            orderDto.EmployeeID = order.EmployeeID;
            orderDto.OrderDate = order.OrderDate;
            orderDto.RequiredDate = order.RequiredDate;
            orderDto.ShippedDate = order.ShippedDate;
            orderDto.ShipVia = order.ShipVia;
            orderDto.Freight = order.Freight;
            orderDto.ShipName = order.ShipName;
            orderDto.ShipAddress = order.ShipAddress;
            orderDto.ShipCity = order.ShipCity;
            orderDto.ShipRegion = order.ShipRegion;
            orderDto.ShipPostalCode = order.ShipPostalCode;
            orderDto.ShipCountry = order.ShipCountry;

            return Ok(orderDto);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrders(int id, OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderDto.OrderID)
            {
                return BadRequest();
            }

            var order = _Repository.ObterPorID(id);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderID = orderDto.OrderID;
            order.CustomerID = orderDto.CustomerID;
            order.EmployeeID = orderDto.EmployeeID;
            order.OrderDate = orderDto.OrderDate;
            order.RequiredDate = orderDto.RequiredDate;
            order.ShippedDate = orderDto.ShippedDate;
            order.ShipVia = orderDto.ShipVia;
            order.Freight = orderDto.Freight;
            order.ShipName = orderDto.ShipName;
            order.ShipAddress = orderDto.ShipAddress;
            order.ShipCity = orderDto.ShipCity;
            order.ShipRegion = orderDto.ShipRegion;
            order.ShipPostalCode = orderDto.ShipPostalCode;
            order.ShipCountry = orderDto.ShipCountry;

            _Repository.Alterar(order);

            try
            {
                _Repository.Salvar();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Orders
        [ResponseType(typeof(OrderDto))]
        public IHttpActionResult PostOrders(OrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = new Orders();

            order.OrderID = orderDto.OrderID;
            order.CustomerID = orderDto.CustomerID;
            order.EmployeeID = orderDto.EmployeeID;
            order.OrderDate = orderDto.OrderDate;
            order.RequiredDate = orderDto.RequiredDate;
            order.ShippedDate = orderDto.ShippedDate;
            order.ShipVia = orderDto.ShipVia;
            order.Freight = orderDto.Freight;
            order.ShipName = orderDto.ShipName;
            order.ShipAddress = orderDto.ShipAddress;
            order.ShipCity = orderDto.ShipCity;
            order.ShipRegion = orderDto.ShipRegion;
            order.ShipPostalCode = orderDto.ShipPostalCode;
            order.ShipCountry = orderDto.ShipCountry;


            _Repository.Inserir(order);
            _Repository.Salvar();


            return CreatedAtRoute("DefaultApi", new { id = order.OrderID }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(OrderDto))]
        public IHttpActionResult DeleteOrders(int id)
        {
            var order = _Repository.ObterPorID(id);

            if (order == null)
            {
                return NotFound();
            }

            _Repository.Apagar(id);
            _Repository.Salvar();

            var orderDto = new OrderDto();

            orderDto.OrderID = order.OrderID;
            orderDto.CustomerID = order.CustomerID;
            orderDto.EmployeeID = order.EmployeeID;
            orderDto.OrderDate = order.OrderDate;
            orderDto.RequiredDate = order.RequiredDate;
            orderDto.ShippedDate = order.ShippedDate;
            orderDto.ShipVia = order.ShipVia;
            orderDto.Freight = order.Freight;
            orderDto.ShipName = order.ShipName;
            orderDto.ShipAddress = order.ShipAddress;
            orderDto.ShipCity = order.ShipCity;
            orderDto.ShipRegion = order.ShipRegion;
            orderDto.ShipPostalCode = order.ShipPostalCode;
            orderDto.ShipCountry = order.ShipCountry;

            return Ok(orderDto);
        }

        // GET: api/Orders/GetByName/{name}
        [ResponseType(typeof(OrderDto))]
        [Route("api/Orders/GetByName/{name}")]
        public IHttpActionResult GetByName(string name)
        {
            var orders = _Repository.ObterTodos().Where(order => order.ShipName.ToLower().Trim().Contains(name.ToLower().Trim()));
            var ordersDto = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto();
                orderDto.OrderID = order.OrderID;
                orderDto.CustomerID = order.CustomerID;
                orderDto.EmployeeID = order.EmployeeID;
                orderDto.OrderDate = order.OrderDate;
                orderDto.RequiredDate = order.RequiredDate;
                orderDto.ShippedDate = order.ShippedDate;
                orderDto.ShipVia = order.ShipVia;
                orderDto.Freight = order.Freight;
                orderDto.ShipName = order.ShipName;
                orderDto.ShipAddress = order.ShipAddress;
                orderDto.ShipCity = order.ShipCity;
                orderDto.ShipRegion = order.ShipRegion;
                orderDto.ShipPostalCode = order.ShipPostalCode;
                orderDto.ShipCountry = order.ShipCountry;

                ordersDto.Add(orderDto);
            }

            return Ok(ordersDto);
        }
        private bool OrdersExists(int id)
        {
            var order = _Repository.ObterPorID(id);
            if (order != null)
            {
                return true;
            }
            return false;
        }
    }
}