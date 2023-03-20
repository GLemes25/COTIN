using Northwind.Data.Logic.Data;
using Northwind.Data.Logic.Interface;
using System;


namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IRepository<Product> _Repository = new Repository<Product>();
            var instancia = _Repository.ObterPorID(1);
            Console.WriteLine(instancia.CategoryID);
            //Console.WriteLine(instancia.CategoryName);

            Console.WriteLine("=============================");

            Console.ReadKey();
        }
    }   
}
