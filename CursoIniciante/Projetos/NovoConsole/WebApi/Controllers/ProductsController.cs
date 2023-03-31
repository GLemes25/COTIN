using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        IRepository<Products> _Repository = new Repository<Products>();

        // GET: api/Products <---> IQueryable: Ele faz uma chamada no banco, lista paginada. Os registros não ficam ali, ele traz so a quantidade.
        public IQueryable<ProductsDto> GetProducts()
        {
            var products = _Repository.ObterTodos();
            var productsDto = new List<ProductsDto>();

            foreach (var product in products)
            {
                var productDto = new ProductsDto();
                productDto.ProductID = product.ProductID;
                productDto.ProductName = product.ProductName;
                productDto.SupplierID = product.SupplierID;
                productDto.CategoryID = product.CategoryID;
                productDto.QuantityPerUnit = product.QuantityPerUnit;
                productDto.UnitPrice = product.UnitPrice;
                productDto.UnitsInStock = product.UnitsInStock;
                productDto.UnitsOnOrder = product.UnitsOnOrder;
                productDto.ReorderLevel = product.ReorderLevel;
                productDto.Discontinued = product.Discontinued;

                productsDto.Add(productDto);
            }
            return productsDto.AsQueryable();
        }

        // GET: api/Products/5
        [ResponseType(typeof(ProductsDto))]
        public IHttpActionResult GetProducts(int id)
        {
            var produtos = _Repository.ObterPorID(id);

            if (produtos == null)
            {
                return NotFound();
            }
            var productDto = new ProductsDto();

            productDto.ProductID = produtos.ProductID;
            productDto.ProductName = produtos.ProductName;
            productDto.SupplierID = produtos.SupplierID;
            productDto.CategoryID = produtos.CategoryID;
            productDto.QuantityPerUnit = produtos.QuantityPerUnit;
            productDto.UnitPrice = produtos.UnitPrice;
            productDto.UnitsInStock = produtos.UnitsInStock;
            productDto.UnitsOnOrder = produtos.UnitsOnOrder;
            productDto.ReorderLevel = produtos.ReorderLevel;
            productDto.Discontinued = produtos.Discontinued;

            return Ok(productDto);

        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProducts(int id, ProductsDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productDto.ProductID)
            {
                return BadRequest();
            }

            var produtos = _Repository.ObterPorID(id);

            if (produtos == null)
            {
                return NotFound();
            }
            var products = new Products();

            products.ProductID = productDto.ProductID;
            products.ProductName = productDto.ProductName;
            products.SupplierID = productDto.SupplierID;
            products.CategoryID = productDto.CategoryID;
            products.QuantityPerUnit = productDto.QuantityPerUnit;
            products.UnitPrice = productDto.UnitPrice;
            products.UnitsInStock = productDto.UnitsInStock;
            products.UnitsOnOrder = productDto.UnitsOnOrder;
            products.ReorderLevel = productDto.ReorderLevel;
            products.Discontinued = productDto.Discontinued;

            _Repository.Alterar(products);

            try
            {
                _Repository.Salvar();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        [ResponseType(typeof(ProductsDto))]
        public IHttpActionResult PostProducts(ProductsDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var products = new Products();

            products.ProductID = productDto.ProductID;
            products.ProductName = productDto.ProductName;
            products.SupplierID = productDto.SupplierID;
            products.CategoryID = productDto.CategoryID;
            products.QuantityPerUnit = productDto.QuantityPerUnit;
            products.UnitPrice = productDto.UnitPrice;
            products.UnitsInStock = productDto.UnitsInStock;
            products.UnitsOnOrder = productDto.UnitsOnOrder;
            products.ReorderLevel = productDto.ReorderLevel;
            products.Discontinued = productDto.Discontinued;

            _Repository.Inserir(products);
            _Repository.Salvar();

            return CreatedAtRoute("DefaultApi", new { id = products.ProductID }, products);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(ProductsDto))]
        public IHttpActionResult DeleteProducts(int id)
        {
            var products = _Repository.ObterPorID(id);

            if (products == null)
            {
                return NotFound();
            }

            _Repository.Apagar(id);
            _Repository.Salvar();

            var productDto = new ProductsDto();

            productDto.ProductID = products.ProductID;
            productDto.ProductName = products.ProductName;
            productDto.SupplierID = products.SupplierID;
            productDto.CategoryID = products.CategoryID;
            productDto.QuantityPerUnit = products.QuantityPerUnit;
            productDto.UnitPrice = products.UnitPrice;
            productDto.UnitsInStock = products.UnitsInStock;
            productDto.UnitsOnOrder = products.UnitsOnOrder;
            productDto.ReorderLevel = products.ReorderLevel;
            productDto.Discontinued = products.Discontinued;

            return Ok(productDto);
        }
        private bool ProductsExists(int id)
        {
            var produtos = _Repository.ObterPorID(id);
            if (produtos != null)
            {
                return true;
            }
            return false;

        }
    }
}