using AutoMapper;
using Common.Api.Logic.Business.Intefaces;
using Common.Api.Logic.Controllers;
using Common.Extensions.Logic;
using Common.UnitOfWork.Collections;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Model;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class CategoriesController : BaseApiController
    {

        private readonly ICategoriesServices _categoriesServices;
        private readonly IMapper _mapper;
        private ILogger<CategoriesController> _logger;

        public CategoriesController(ICategoriesServices categoriesServices, IMapper mapper, ILogger<CategoriesController> logger, INotification notification) : base(notification)
        {
            _categoriesServices = categoriesServices;
            _mapper = mapper;
            _logger = logger;

        }

        /// <summary>
        /// Método GET, Lista com todos os registros.
        /// </summary>
        /// <param name="Categories">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IList<Categories>>> GetCategories()
        {
            _logger.LogInformation((int)LogLevel.Information, "Método GetAll - Lista de Categorias");

            var categories = await _categoriesServices.GET_ListaCategories();

            if (categories == null)
            {
                return NotFound();
            }
            return CustomResponse(categories);
        }

        /// <summary>
        /// Método GET, destinado a busca de registros, com paginação.
        /// </summary>
        /// <param name="pageNo">Número da página que será exibida (default = 1).</param>
        /// <param name="pageSize">Quantidade de registros que serão exibidos (default = 20).</param>
        /// <returns>O envelope JSON correspondente ao resultado da pesquisa.</returns>
        //GET: paginação/Categories
        [HttpGet("ListagemCategoria")]
        public async Task<ActionResult<IPagedList<Categories>>> Get_ListaCategoriesPage(int? pageNo = 1, int? pageSize = 20)
        {
            _logger.LogInformation((int)LogLevel.Information, "Método GetAll - Listagem de Categorias PageNumber {0} PageSize {1} ", pageNo, pageSize);

            var categories = await _categoriesServices.GET_CategoriesPage(pageNo, pageSize);

            if (categories == null)
            {
                return NotFound();
            }

            return CustomResponse(categories);
        }

        /// <summary>
        /// Método GET, destinado a busca de registros por ID.
        /// </summary>
        /// <param name="id">Chave primária da tabela.</param>
        /// <returns>O envelope JSON correspondente ao resultado da pesquisa.</returns>
        // GET: api/Categories/5/ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            _logger.LogInformation((int)LogLevel.Information, "Método GET - Lista por ID {0} ", id);

            var categories = await _categoriesServices.GET_Categories(id);

            if (categories == null)
            {
                return NotFound();
            }

            return CustomResponse(categories);
        }

        /// <summary>
        /// Método PUT, destinado a alteração de registros no banco.
        /// </summary>
        /// <param name="Categories">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCategories([FromBody] CategoriesViewModel categories) //"frombody" é um formulário que vem na requisição, um modelo que vem no corpo da requisição
        {
            _logger.LogInformation((int)LogLevel.Information, string.Format("Categorias: put({0})", JsonConvert.SerializeObject(categories)));

            var id = await _categoriesServices.GET_Categories(categories.CategoryID);

            if (id == null)
            {
                return NotFound();
            }

            var instancia = _mapper.Map<Categories>(categories);
            var categoriesViewModel = await _categoriesServices.PUT_Categories(instancia);


            return CustomResponse(categories);
        }

        /// <summary>
        /// Método POST, destinado a criação de registros.
        /// </summary>
        /// <param name="Categories">Instância em JSON do registro para inclusão.</param>
        /// <returns>O envelope JSON do registro incluso com Chave Primária.</returns>
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriesViewModel>> PostCategories(CategoriesViewModel categories)
        {
            _logger.LogInformation((int)LogLevel.Information, string.Format("Categorias: Post({0})", JsonConvert.SerializeObject(categories)));

            if (categories == null)
            {
                return Problem("Entity set 'NorthWindContext.Categories'  is null.");
            }

            var instancia = _mapper.Map<Categories>(categories);
            var categoriesViewModel = await _categoriesServices.POST_Categories(instancia);

            return CustomResponse(categories);

            //return Ok(instancia); //ok retorna um objeto
        }

        /// <summary>
        /// Método DELETE, destinado a exclusão de registros do banco, utilizando o ID.
        /// </summary>
        /// <param name="id">Chave primária do registro.</param>
        /// <returns>O envelope JSON com registro excluído.</returns>
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            _logger.LogInformation((int)LogLevel.Information, "Categorias: Delete({0}) ", id);

            var categories = await _categoriesServices.GET_Categories(id);

            if (categories == null)
            {
                return NotFound();
            }

            await _categoriesServices.DELETE_Categories(id);

            return CustomResponse(categories);
        }
    }
}
