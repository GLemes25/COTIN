using ConsoleAppDemo.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo1.Logic
{
    public class CategorieService
    {
        private List<Categories> lista = new List<Categories>();
        public CategorieService()
        {
            CarregarListadecategoria();
        }

        public void InserirCategoria(Categories categories)
        {
            lista.Add(categories);
        }

        public void RemoverCategoria(Categories categories)
        {
            lista.Remove(categories);
        }

        public Categories GetCategorias(int id)
        {
            return lista.Where(x => x.CategoryID == id).FirstOrDefault();
        }

        public List<Categories> GetCategorias(string categoriaName)
        {
            return lista.Where(x => x.CategoryName.Contains(categoriaName)).ToList();
        }


        public List<Categories> ObterListadecategoria()
        {
            return lista;
        }

        private void CarregarListadecategoria()
        {
            var cat1 = new Categories() { CategoryID = 1, CategoryName = "Beverages", Description = "Soft drinks, coffees, teas, beers, and ales" };
            var cat2 = new Categories() { CategoryID = 2, CategoryName = "Condiments", Description = "Sweet and savory sauces, relishes, spreads, and seasonings" };
            var cat3 = new Categories() { CategoryID = 3, CategoryName = "Confections", Description = "Desserts, candies, and sweet breads" };
            var cat4 = new Categories() { CategoryID = 4, CategoryName = "Dairy Products", Description = "Cheeses" };
            var cat5 = new Categories() { CategoryID = 5, CategoryName = "Grains/Cereals", Description = "Breads, crackers, pasta, and cereal" };
            var cat6 = new Categories() { CategoryID = 6, CategoryName = "Meat/Poultry", Description = "Prepared meats" };
            var cat7 = new Categories() { CategoryID = 7, CategoryName = "Produce", Description = "Dried fruit and bean curd" };
            var cat8 = new Categories() { CategoryID = 8, CategoryName = "Seafood", Description = "Seaweed and fish" };


            lista.Add(cat1);
            lista.Add(cat2);
            lista.Add(cat3);
            lista.Add(cat4);
            lista.Add(cat5);
            lista.Add(cat6);
            lista.Add(cat7);
            lista.Add(cat8);
        }
    }
}
