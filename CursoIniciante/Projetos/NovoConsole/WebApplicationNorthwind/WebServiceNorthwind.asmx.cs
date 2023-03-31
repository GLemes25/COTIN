using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebApplicationNorthwind.Model;

namespace WebApplicationNorthwind
{
    /// <summary>
    /// Summary description for WebServiceNorthwind
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceNorthwind : System.Web.Services.WebService
    {

        [WebMethod]
        public CategoriesDto ObterCategoriaPorId(int categoryId)
        {
            IRepository<Categories> _Repository = new Repository<Categories>();

            CategoriesDto categoriesDto = new CategoriesDto();
            var categoria = _Repository.ObterPorID(categoryId);

            if (categoria != null)
            {

                categoriesDto.CategoryID = categoria.CategoryID;
                categoriesDto.CategoryName = categoria.CategoryName;
                categoriesDto.Description = categoria.Description;

            }
            return categoriesDto;

        }

        [WebMethod]
        public ProductsDto ObterProdutoPorId(int productId)
        {
            IRepository<Products> _Repository = new Repository<Products>();

            ProductsDto productsDto = new ProductsDto();
            var produto = _Repository.ObterPorID(productId);

            if (produto != null)
            {
                productsDto.ProductID = produto.ProductID;
                productsDto.ProductName = produto.ProductName;
                productsDto.SupplierID = produto.SupplierID;
                productsDto.CategoryID = produto.CategoryID;
                productsDto.Category.CategoryID = produto.Categories.CategoryID; 
                productsDto.Category.CategoryName = produto.Categories.CategoryName;
                productsDto.Category.Description= produto.Categories.Description;

            }
            return productsDto;

        }

        [WebMethod]
        public List<CategoriesDto> ObterListaDeCategorias()
        {
            IRepository<Categories> _Repository = new Repository<Categories>();

            List<CategoriesDto> categoriesDto = new List<CategoriesDto>();
            var categorias = _Repository.ObterTodos();

            if (categorias.Count() != 0)
            {
                foreach (var item in categorias)
                {
                    var categoria = new CategoriesDto();
                    categoria.CategoryID = item.CategoryID;
                    categoria.CategoryName = item.CategoryName;
                    categoria.Description = item.Description;

                    categoriesDto.Add(categoria);
                }

            }
            return categoriesDto;

        }


        [WebMethod]
        public List<ProductsDto> ObterListaDeProdutos()
        {
            IRepository<Products> _Repository = new Repository<Products>();

            List<ProductsDto> productsDbo = new List<ProductsDto>();
            var productos = _Repository.ObterTodos();

            if (productos.Count() != 0)
            {
                foreach (var item in productos)
                {
                    var newProduct = new ProductsDto();
                    newProduct.ProductID = item.ProductID;
                    newProduct.ProductName = item.ProductName;
                    newProduct.SupplierID = item.SupplierID;
                    newProduct.CategoryID = item.CategoryID;
                    newProduct.Category.CategoryID = item.Categories.CategoryID;
                    newProduct.Category.CategoryName = item.Categories.CategoryName;
                    newProduct.Category.Description = item.Categories.Description;

                    productsDbo.Add(newProduct);
                }

            }
            return productsDbo;

        }
    }
}
