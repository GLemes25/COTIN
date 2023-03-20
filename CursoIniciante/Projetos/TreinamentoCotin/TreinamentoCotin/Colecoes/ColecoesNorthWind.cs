using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TreinamentoCotin.Data.Northwind;

namespace TreinamentoCotin.Colecoes
{
    internal class ColecoesNorthWind
    {
        //Console.WriteLine("== Aprovados ===============");
        //    var aprovados = alunos.Where(a => a.Nota >= 7)
        //        .OrderBy(a => a.Nome);
        //var chamada = alunos.OrderBy(a => a.Nome).Select(a => a.Nome);
        public static void Executar()
        {
            string erros = string.Empty;

            try
            {
                var services = new CategoriesService();
              
                var products = services.ObterProdutosporID(1);


                foreach (var product in products)
               {
                Console.WriteLine(  );
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.ProductName);

                    Console.WriteLine("------------------------------------");
               }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro ao realizar a Alteração!");
                Console.WriteLine("Causa: " + erros);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (string.IsNullOrEmpty(erros))
                {
                    Console.WriteLine("Obrigado!");
                }
                else
                {
                    Console.WriteLine("Não foi possivel concluir sua alteração");
                }

            }


            //var categorias = services.ObterListadeCategorias();

            //foreach (var categoria in categorias)
            //{
            //    Console.WriteLine(categoria.CategoryName);
            //    Console.WriteLine(categoria.CategoryID);
            //    Console.WriteLine(categoria.Description);
            //}
        }

        public class CategoriesService
        {
            private static NorthwindEntities northwindEntities;
            public CategoriesService()
            {
                northwindEntities = new NorthwindEntities();
            }

            public Categories ObterCategoriasporID(int categoryID)
            {
                var categorias = northwindEntities.Categories.First(x => x.CategoryID == categoryID);
                return categorias;
            }
            public ICollection<Products> ObterProdutosporID(int categoryID)
            {
                var produtos = northwindEntities.Products.Where(x => x.CategoryID == categoryID);
                return produtos.ToList();
            }
         

            public List<Categories> ObterListadeCategorias()
            {
                var categorias = northwindEntities.Categories;
                return categorias.ToList();
            }


            public void AlterarCategoria(Categories categories)
            {
                northwindEntities.Categories.AddOrUpdate(categories);
                northwindEntities.SaveChanges();
            }

            public void IncluirCategoria(Categories categories)
            {
                northwindEntities.Categories.AddOrUpdate(categories);
                northwindEntities.SaveChanges();
            }
            public void ExcluirCategoria(Categories categories)
            {
                northwindEntities.Categories.Remove(categories);
                northwindEntities.SaveChanges();
            }
        }

    }
}