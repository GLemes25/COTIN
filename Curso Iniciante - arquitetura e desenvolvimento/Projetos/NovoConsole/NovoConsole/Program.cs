using Northwind.Business.Logic.Validation;
using Northwind.Data.Logic;
using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using Northwind.Data.Logic.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovoConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRepository<Products> _Repository = new Repository<Products>();
            var Product = _Repository.GetByID(1);

            CategoriesValidation validation = new CategoriesValidation();
            var result = validation.Validate(Product);

            if (result.IsValid)
            {
                Console.WriteLine("Product ID : {0}", Product.ProductID);
                Console.WriteLine("Product Name : {0}", Product.ProductName);
            }
            else { Console.WriteLine(result); }
            Console.WriteLine("=============================");

            Console.ReadKey();
        }
    }
}
