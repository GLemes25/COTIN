using AutoMapper;
using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Controllers;
using Common.UnitOfWork.Collections;
using Microsoft.AspNetCore.Mvc;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Model;
using Northwind.API.Logic.Services;
using NorthWind.Data.Logic.Data.NorthWind.Entity;

namespace Northwind.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductsServices _productsServices;
        private readonly IMapper _mapper;
        private ILogger<ProductsController> _logger;

        public ProductsController(IProductsServices productsService, IMapper mapper, ILogger<ProductsController> logger, INotification notification) : base(notification)
        {
            _productsServices = productsService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Método GET, Lista com todos os registros.
        /// </summary>
        /// <param name="Products">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IList<Products>>> GetProducts()
        {
            var products = await _productsServices.GET_ListaProducts();

            if (products == null)
            {
                return NotFound();
            }

            return CustomResponse(products);
        }

        /// <summary>
        /// Método GET, destinado a busca de registros, com paginação.
        /// </summary>
        /// <param name="pageNo">Número da página que será exibida (default = 1).</param>
        /// <param name="pageSize">Quantidade de registros que serão exibidos (default = 20).</param>
        /// <returns>O envelope JSON correspondente ao resultado da pesquisa.</returns>
        //GET: paginação/Products
        [HttpGet("ListagemProdutos")]
        public async Task<ActionResult<IPagedList<Products>>> Get_ListaProductsPage(int? pageNo = 1, int? pageSize = 20)
        {
            var products = await _productsServices.GET_ProductsPage(pageNo, pageSize);

            if (products == null)
            {
                return NotFound();
            }

            return CustomResponse(products);
        }
        /// <summary>
        /// Método GET, destinado a busca de registros por ID.
        /// </summary>
        /// <param name="id">Chave primária da tabela.</param>
        /// <returns>O envelope JSON correspondente ao resultado da pesquisa.</returns>
        /// GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _productsServices.GET_Products(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }
        /// <summary>
        /// Método PUT, destinado a alteração de registros no banco.
        /// </summary>
        /// <param name="Products">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("AlterarProduto")]
        public async Task<IActionResult> PutProducts([FromBody] ProductsViewModel products) //"frombody" é um formulário que vem na requisição, um modelo que vem no corpo da requisição
        {
            var id = await _productsServices.GET_Products(products.ProductID);

            if (id == null)
            {
                return NotFound();
            }

            var instancia = _mapper.Map<Products>(products);
            var productsViewModel = await _productsServices.PUT_Products(instancia);

            return CustomResponse(instancia);
        }

        /// <summary>
        /// Método POST, destinado a criação de registros.
        /// </summary>
        /// <param name="Products">Instância em JSON do registro para inclusão.</param>
        /// <returns>O envelope JSON do registro incluso com Chave Primária.</returns>
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InserirProduto")]
        public async Task<ActionResult<ProductsViewModel>> PostProducts(ProductsViewModel products)
        {
            if (products == null)
            {
                return Problem("Entity set 'NorthWindContext.Products'  is null.");
            }

            var instancia = _mapper.Map<Products>(products);
            var productsViewModel = await _productsServices.POST_Products(instancia);


            return CustomResponse(instancia);
        }

        /// <summary>
        /// Método DELETE, destinado a exclusão de registros do banco, utilizando o ID.
        /// </summary>
        /// <param name="id">Chave primária do registro.</param>
        /// <returns>O envelope JSON com registro excluído.</returns>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var products = await _productsServices.GET_Products(id);

            if (products == null)
            {
                return NotFound();
            }

            await _productsServices.DELETE_Products(id);

            return CustomResponse(products);
        }
    }
}
