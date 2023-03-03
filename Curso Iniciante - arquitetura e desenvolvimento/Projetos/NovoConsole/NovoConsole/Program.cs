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
            IRepository<Categories> _Repository = new Repository<Categories>();
            var Category = _Repository.GetByID(1);

            Category.CategoryName = "dsadsadsadsaasscassadadasdasadadadadaxasxadsadasasfsfdf";

            CategoriesValidation validation = new CategoriesValidation();
            var result = validation.Validate(Category);   

            Console.WriteLine("Category ID : {0}", Category.CategoryID);
            Console.WriteLine("Category Name : {0}", Category.CategoryName);

            Console.WriteLine("=============================");

            Console.ReadKey();
        }
    }
}
