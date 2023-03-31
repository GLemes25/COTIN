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
    public class SuppliersController : ApiController
    {
        IRepository<Suppliers> _Repository = new Repository<Suppliers>();

        // GET: api/Suppliers
        public IQueryable<SuppliersDto> GetSuppliers()
        {
            var suppliers = _Repository.ObterTodos();
            var suppliersDto = new List<SuppliersDto>();

            foreach (var supplier in suppliers)
            {
                var supplierDto = new SuppliersDto();

                supplierDto.City = supplier.City;
                supplierDto.Address = supplier.Address;
                supplierDto.Country = supplier.Country;
                supplierDto.SupplierID = supplier.SupplierID;
                supplierDto.PostalCode = supplier.PostalCode;
                supplierDto.Phone = supplier.Phone;
                supplierDto.Fax = supplier.Fax;
                supplierDto.CompanyName = supplier.CompanyName;
                supplierDto.ContactName = supplier.ContactName;
                supplierDto.ContactTitle = supplier.ContactTitle;
                supplierDto.HomePage = supplier.HomePage;
                supplierDto.Region = supplier.Region;

                suppliersDto.Add(supplierDto);
            }
            return suppliersDto.AsQueryable();
        }

        // GET: api/Suppliers/5
        [ResponseType(typeof(SuppliersDto))]
        public IHttpActionResult GetSuppliers(int id)
        {
            var suppliers = _Repository.ObterPorID(id);

            if (suppliers == null)
            {
                return NotFound();
            }
            var supplierDto = new SuppliersDto();

            supplierDto.City = suppliers.City;
            supplierDto.Address = suppliers.Address;
            supplierDto.Country = suppliers.Country;
            supplierDto.SupplierID = suppliers.SupplierID;
            supplierDto.PostalCode = suppliers.PostalCode;
            supplierDto.Phone = suppliers.Phone;
            supplierDto.Fax = suppliers.Fax;
            supplierDto.CompanyName = suppliers.CompanyName;
            supplierDto.ContactName = suppliers.ContactName;
            supplierDto.ContactTitle = suppliers.ContactTitle;
            supplierDto.HomePage = suppliers.HomePage;
            supplierDto.Region = suppliers.Region;

            return Ok(supplierDto);
        }

        // PUT: api/Suppliers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuppliers(int id, SuppliersDto supplierDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!SuppliersExists(supplierDto.SupplierID))
            {
                return NotFound();
            }
            if (id != supplierDto.SupplierID)
            {
                return BadRequest();
            }

            var supplier = new Suppliers();

            supplier.SupplierID = supplierDto.SupplierID;
            supplier.PostalCode = supplierDto.PostalCode;
            supplier.Phone = supplierDto.Phone;
            supplier.Fax = supplierDto.Fax;
            supplier.CompanyName = supplierDto.CompanyName;
            supplier.Address = supplierDto.Address;
            supplier.City = supplierDto.City;
            supplier.Region = supplierDto.Region;
            supplier.Country = supplierDto.Country;
            supplier.ContactName = supplierDto.ContactName;
            supplier.ContactTitle = supplierDto.ContactTitle;
            supplier.HomePage = supplierDto.HomePage;

            _Repository.Alterar(supplier);

            try
            {
                _Repository.Salvar();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(id))
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

        // POST: api/Suppliers
        [ResponseType(typeof(SuppliersDto))]
        public IHttpActionResult PostSuppliers(SuppliersDto suppliersDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplier = new Suppliers();

            supplier.SupplierID = suppliersDto.SupplierID;
            supplier.PostalCode = suppliersDto.PostalCode;
            supplier.Phone = suppliersDto.Phone;
            supplier.Fax = suppliersDto.Fax;
            supplier.CompanyName = suppliersDto.CompanyName;
            supplier.Address = suppliersDto.Address;
            supplier.City = suppliersDto.City;
            supplier.Region = suppliersDto.Region;
            supplier.Country = suppliersDto.Country;
            supplier.ContactName = suppliersDto.ContactName;
            supplier.ContactTitle = suppliersDto.ContactTitle;
            supplier.HomePage = suppliersDto.HomePage;

            _Repository.Inserir(supplier);
            _Repository.Salvar();

            return CreatedAtRoute("DefaultApi", new { id = supplier.SupplierID }, supplier);
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(SuppliersDto))]
        public IHttpActionResult DeleteSuppliers(int id)
        {
            var suppliers = _Repository.ObterPorID(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            _Repository.Apagar(id);
            _Repository.Salvar();

            var supplierDto = new SuppliersDto();

            supplierDto.City = suppliers.City;
            supplierDto.Address = suppliers.Address;
            supplierDto.Country = suppliers.Country;
            supplierDto.SupplierID = suppliers.SupplierID;
            supplierDto.PostalCode = suppliers.PostalCode;
            supplierDto.Phone = suppliers.Phone;
            supplierDto.Fax = suppliers.Fax;
            supplierDto.CompanyName = suppliers.CompanyName;
            supplierDto.ContactName = suppliers.ContactName;
            supplierDto.ContactTitle = suppliers.ContactTitle;
            supplierDto.HomePage = suppliers.HomePage;
            supplierDto.Region = suppliers.Region;

            return Ok(supplierDto);
        }
        private bool SuppliersExists(int id)
        {
            var     supplier = _Repository.ObterPorID(id);
            if (supplier != null)
            {
                return true;
            }
            return false;
        }
    }
}