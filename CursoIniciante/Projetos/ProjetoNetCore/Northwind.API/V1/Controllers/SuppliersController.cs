using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Model;
using Northwind.API.Logic.Services;
using Northwind.Data.Logic.Data.Northwind.Context;
using NorthWind.Data.Logic.Data.NorthWind.Entity;

namespace Northwind.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class SuppliersController : BaseApiController
    {
        private readonly ISuppliersServices _suppliersServices;
        private readonly IMapper _mapper;
        private ILogger<SuppliersController> _logger;

        public SuppliersController(ISuppliersServices suppliersServices, IMapper mapper, ILogger<SuppliersController> logger, INotification notification) : base(notification)
        {
            _logger = logger;
            _mapper = mapper;
            _suppliersServices = suppliersServices;
        }

        /// <summary>
        /// Método GET, Lista com todos os registros.
        /// </summary>
        /// <param name="Suppliers">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IList<Suppliers>>> GetSuppliers()
        {
            var suppliers = await _suppliersServices.GET_ListaSuppliers();

            if (suppliers == null)
            {
                return NotFound();
            }
            return CustomResponse(suppliers);
        }

        /// <summary>
        /// Método GET, destinado a busca de registros por ID.
        /// </summary>
        /// <param name="id">Chave primária da tabela.</param>
        /// <returns>O envelope JSON correspondente ao resultado da pesquisa.</returns>
        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Suppliers>> GetSuppliers(int id)
        {
            var suppliers = await _suppliersServices.GET_Suppliers(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            return CustomResponse(suppliers);
        }

        /// <summary>
        /// Método PUT, destinado a alteração de registros no banco.
        /// </summary>
        /// <param name="Suppliers">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSuppliers([FromBody] SuppliersViewModel suppliers)
        {
            var id = await _suppliersServices.GET_Suppliers(suppliers.SupplierID);

            if (id == null)
            {
                return NotFound();
            }
            var instancia = _mapper.Map<Suppliers>(suppliers);
            var productsViewModel = await _suppliersServices.PUT_Suppliers(instancia);


            return CustomResponse(suppliers);
        }

        /// <summary>
        /// Método POST, destinado a criação de registros.
        /// </summary>
        /// <param name="Suppliers">Instância em JSON do registro para inclusão.</param>
        /// <returns>O envelope JSON do registro incluso com Chave Primária.</returns>
        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Suppliers>> PostSuppliers([FromBody] SuppliersViewModel suppliers)
        {

            if (suppliers == null)
            {
                return Problem("Entity set 'NorthWindContext.Suppliers'  is null.");
            }
            var instancia = _mapper.Map<Suppliers>(suppliers);
            var suppliersViewModel = await _suppliersServices.PUT_Suppliers(instancia);


            return CustomResponse(suppliers);
        }

        /// <summary>
        /// Método DELETE, destinado a exclusão de registros do banco, utilizando o ID.
        /// </summary>
        /// <param name="id">Chave primária do registro.</param>
        /// <returns>O envelope JSON com registro excluído.</returns>
        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuppliers(int id)
        {
            var suppliers = await _suppliersServices.GET_Suppliers(id);

            if (suppliers == null)
            {
                return NotFound();
            }

            await _suppliersServices.DELETE_Suppliers(id);

            return CustomResponse(suppliers);
        }

    }
}
