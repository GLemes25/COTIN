using ConsoleApp2.Data;
using ConsoleApp2.Data.Interface;
using ConsoleApp2.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IRepository<Category> _Repository = new Repository<Category>();
            var instancia = _Repository.GetById(1);
            Console.WriteLine(instancia.CategoryID);
            Console.WriteLine(instancia.CategoryName);

            Console.WriteLine("=============================");
            instancia.CategoryName = "Test";
            _Repository.Update(instancia);
            var instancia2 = _Repository.GetById(1);

            //foreach (var item in instancia)
            //{
            Console.WriteLine(instancia2.CategoryID);
            Console.WriteLine(instancia2.CategoryName);

            Console.WriteLine("=============================");
            //}
            Console.ReadKey();
        }
    }
}
