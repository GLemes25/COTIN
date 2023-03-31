using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        IRepository<Categories> _Repository = new Repository<Categories>();

        // GET: api/Categories
        public IQueryable<CategoriesDto> GetCategories()
        {
            var categories = _Repository.ObterTodos();
            var categoriesDto = new List<CategoriesDto>();

            foreach (var category in categories)
            {
                var categoryDto = new CategoriesDto();
                categoryDto.CategoryID = category.CategoryID;
                categoryDto.CategoryName = category.CategoryName;
                categoryDto.Description = category.Description;

                categoriesDto.Add(categoryDto);
            }
            return categoriesDto.AsQueryable();
        }

        // GET: api/Categories/5
        [ResponseType(typeof(CategoriesDto))]
        public IHttpActionResult GetCategories(int id)
        {
            var category = _Repository.ObterPorID(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = new CategoriesDto();

            categoryDto.CategoryID = category.CategoryID;
            categoryDto.CategoryName = category.CategoryName;
            categoryDto.Description = category.Description;

            return Ok(categoryDto);
        }

        // PUT: api/Categories/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategories(int id, CategoriesDto categoriesDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriesDto.CategoryID)
            {
                return BadRequest();
            }

            var category = _Repository.ObterPorID(id);

            if (category == null)
            {
                return NotFound();
            }

            category.CategoryID = categoriesDto.CategoryID;
            category.CategoryName = categoriesDto.CategoryName;
            category.Description = categoriesDto.Description;

            _Repository.Alterar(category);

            try
            {
                _Repository.Salvar();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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
        // POST: api/Categories
        [ResponseType(typeof(CategoriesDto))]
        public IHttpActionResult PostCategories(CategoriesDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = new Categories();

            category.CategoryID = categoryDto.CategoryID;
            category.CategoryName = categoryDto.CategoryName;
            category.Description = categoryDto.Description;


            _Repository.Inserir(category);
            _Repository.Salvar();

            return CreatedAtRoute("DefaultApi", new { id = category.CategoryID }, category);
        }   

        // DELETE: api/Categories/5
        [ResponseType(typeof(CategoriesDto))]
        public IHttpActionResult DeleteCategories(int id)
        {
            var category = _Repository.ObterPorID(id);

            if (category == null)
            {
                return NotFound();
            }

            _Repository.Apagar(id);
            _Repository.Salvar();

            var categoryDto = new CategoriesDto();

            categoryDto.CategoryID = category.CategoryID;
            categoryDto.CategoryName = category.CategoryName;
            categoryDto.Description = category.Description;
  

            return Ok(categoryDto);
        }


        private bool CategoriesExists(int id)
        {
            var category = _Repository.ObterPorID(id);
            if (category != null)
            {
                return true;
            }
            return false;
        }
    }
}