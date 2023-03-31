using ConsoleAppWebService.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWebService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebServiceNorthwindSoapClient client = new WebServiceNorthwindSoapClient();

            var products = client.ObterListaDeProdutos(
                );
            foreach (var product in products)
            {

                Console.WriteLine("\nPRODUTO");
                Console.WriteLine("ProductID  => " + product.ProductID);
                Console.WriteLine("ProductName  => " + product.ProductName);
                Console.WriteLine("SupplierID  => " + product.SupplierID);
                Console.WriteLine("CATEGORIA");
                Console.WriteLine("CategoryID => " + product.Category.CategoryID);
                Console.WriteLine("CategoryName => " + product.Category.CategoryName);
                Console.WriteLine("Description => " + product.Category.Description);
                Console.WriteLine("==============================================");
            }



            Console.ReadKey();
        }
    }
}
