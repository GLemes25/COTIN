using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TreinamentoCotin.Data.Northwind;

namespace TreinamentoCotin.Colecoes
{
    internal class ColecoesNorthWind
    {
        public static void Executar()
        {
            var service = new CategoriesService();
            var categoria=service.ObterCategorias();

            foreach (var c in categoria)
            {
                Console.WriteLine("\n\t A Categoria é : {0} => {1}",c.CategoryID, c.CategoryName);
            }
                Console.ReadKey();
        }

        public class CategoriesService
        {
            private static NorthwindEntities northwindEntities;
            public CategoriesService()
            {
                northwindEntities = new NorthwindEntities();


            }
            public Categories ObterCategoriasPorID(int CategoryID)
            {

                var categorias = northwindEntities.Categories.First(x => x.CategoryID == CategoryID);
                return categorias;

            }

            public List<Categories> ObterCategorias()
            {
                var categorias = northwindEntities.Categories;
                return categorias.ToList();

            }

        }
    }
}
