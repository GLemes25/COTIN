using AutoMapper;
using Common.UnitOfWork.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.API.Logic.Interfaces;
using Northwind.API.Logic.Model;
using Northwind.Data.Logic.Data.Northwind.Context;
using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.API.V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesServices _categoriesServices;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoriesServices categoriesServices, IMapper mapper)
        {
            _categoriesServices = categoriesServices;
            _mapper = mapper;
        }

        /// <summary>
        /// Método GET, Lista com todos os registros.
        /// </summary>
        /// <param name="Employees">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IList<Categories>>> GetCategories()
        {
            var categories = await _categoriesServices.GET_ListaCategories();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
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
            var categories = await _categoriesServices.GET_CategoriesPage(pageNo, pageSize);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
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
            var categories = await _categoriesServices.GET_Categories(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        /// <summary>
        /// Método PUT, destinado a alteração de registros no banco.
        /// </summary>
        /// <param name="Employees">Instância em JSON do registro que será alterado.</param>
        /// <returns>O envelope JSON com registro alterado.</returns>
        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutCategories([FromBody] CategoriesViewModel categories) //"frombody" é um formulário que vem na requisição, um modelo que vem no corpo da requisição
        {
            var id = await _categoriesServices.GET_Categories(categories.CategoryID);

            if (id == null)
            {
                return NotFound();
            }

            var instancia = _mapper.Map<Categories>(categories);
            var categoriesViewModel = await _categoriesServices.PUT_Categories(instancia);


            return Ok(categories);
        }

        /// <summary>
        /// Método POST, destinado a criação de registros.
        /// </summary>
        /// <param name="Employees">Instância em JSON do registro para inclusão.</param>
        /// <returns>O envelope JSON do registro incluso com Chave Primária.</returns>
        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriesViewModel>> PostCategories(CategoriesViewModel categories)
        {
            if (categories == null)
            {
                return Problem("Entity set 'NorthWindContext.Categories'  is null.");
            }

            var instancia = _mapper.Map<Categories>(categories);
            var categoriesViewModel = await _categoriesServices.POST_Categories(instancia);


            return Ok(instancia); //ok retorna um objeto
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
            var categories = await _categoriesServices.GET_Categories(id);

            if (categories == null)
            {
                return NotFound();
            }

            await _categoriesServices.DELETE_Categories(id);

            return Ok(categories);
        }
    }
}
