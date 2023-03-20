using FluentValidation;
using NorthWind.Business.Logic.Validation;
using NorthWind.Data.Logic.Data;
using NorthWind.Data.Logic.Interface;
using NorthWind.Data.Logic.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoProgramaComWilson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //IRepository<Categories> _Repository = new Repository<Categories>();

            //var categoria = _Repository.ObterPorID(1);

            //categoria.CategoryName = "shaushaioiudhsudhasufhoaushdouhasodhuoashodhouasohdouasodohuashodas";
            //categoria.Description = "Texto";
            //categoria.CategoryID = 0;

            //CategoriesValidation validation = new CategoriesValidation();

            //var result = validation.Validate(categoria);

            //foreach(var iten in result.Errors) 
            //{
            //    Console.WriteLine(iten);
            //}

            //Console.WriteLine(categoria.CategoryName);

            IRepository<Products> _Repository = new Repository<Products>();

            var produto = new Products();

            produto.ProductID = 80;
            produto.ProductName = "Arroz";
            produto.QuantityPerUnit = "Oi";
            produto.UnitsInStock = 1000;
            produto.UnitsOnOrder = 200;
            produto.ReorderLevel = 20;
            produto.Discontinued = true;


            ProductsValidation validation = new ProductsValidation();

            var result = validation.Validate(produto);

            foreach (var iten in result.Errors)
            {
                Console.WriteLine(iten);
            }

            Console.WriteLine(produto.ProductID);

            Console.ReadKey();





        }
    }
}
