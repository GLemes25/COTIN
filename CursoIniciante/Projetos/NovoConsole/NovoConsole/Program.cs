using Northwind.Business.Logic.Validation;
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using Northwind.Data.Logic.Repository;
using System;

namespace NovoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region
            //IRepository<Products> _Repository = new Repository<Products>();
            //var Product = _Repository.GetByID(1);

            //CategoriesValidation validation = new CategoriesValidation();
            //var result = validation.Validate(Product);

            //if (result.IsValid)
            //{
            //    Console.WriteLine("Product ID : {0}", Product.ProductID);
            //    Console.WriteLine("Product Name : {0}", Product.ProductName);
            //}
            //else { Console.WriteLine(result); }
            //Console.WriteLine("=============================");

            //Console.ReadKey();
            #endregion

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
