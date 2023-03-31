using Northwind.Business.Logic.Validation;
using NovoConsole.Logic.Services;
using System;
using WebApi.Models;

namespace NovoConsole
{
    internal class Program
    {
        public static CategoriesService CategoriesService { get; private set; }

        static void Main(string[] args)
        {
            CategoriesService = new CategoriesService();

            int option = 0;
            while (option != 6)
            {

                Console.WriteLine("==============================================================================");
                Console.WriteLine("\n\tEscolha uma opção:");
                Console.WriteLine("\t1 - GET: Obter Lista de Categorias");
                Console.WriteLine("\t2 - GET: Obter Categoria por ID");
                Console.WriteLine("\t3 - POST: Adicionar Categoria");
                Console.WriteLine("\t4 - PUT: Alterar Categoria");
                Console.WriteLine("\t5 - DELETE: Excluir Categoria");
                Console.WriteLine("\t6 - Sair\n");
                Console.WriteLine("==============================================================================");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Opção inválida, tente novamente.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        CategoriesList();
                        break;
                    case 2:
                        CategoryByID();
                        break;
                    case 3:
                        AdicionarCategoria();
                        break;
                    case 4:
                        AlterarCategoria();
                        break;
                    case 5:
                        DeletarCategoria();
                        break;
                    case 6:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;
                }
            }

            Console.ReadKey();
        }

        static void CategoryByID()
        {
            var category = CategoriesService.GetCategoryByID(getID());
            showCategory(category);
        }

        static void CategoriesList()
        {
            var categoriesList = CategoriesService.GetCategoriesList();
            foreach (var category in categoriesList)
            {
                showCategory(category);
            }
        }

        static void AdicionarCategoria()
        {
            int id = getID();
            Console.WriteLine("Digite o Nome da Categoria");
            string categoryName = Console.ReadLine();
            Console.WriteLine("Digite a Descrição da Categoria");
            string categoryDescription = Console.ReadLine();
            CategoriesDto category = new CategoriesDto(id, categoryName, categoryDescription);

            var categoryAdd = CategoriesService.AddCategory(category);
            showCategory(categoryAdd);
        }

        static void AlterarCategoria()
        {
            var id = getID();
            Console.WriteLine("Digite o Novo Nome da Categoria");
            string categoryName = Console.ReadLine();
            Console.WriteLine("Digite a Nova Descrição da Categoria");
            string categoryDescription = Console.ReadLine();
            var category = CategoriesService.GetCategoryByID(id);

            category.CategoryName = categoryName;
            category.Description = categoryDescription;


            var newCategory = CategoriesService.UpdateCategory(id, category);


            showCategory(newCategory);

        }

        static void DeletarCategoria()
        {
            var deleted = CategoriesService.DeleteCategoryID(getID());

            string resultado = deleted ? "Categoria Deletada" : "Erro: Não foi possivel deletar categoria";
            Console.WriteLine(resultado);
        }

        static void showCategory(CategoriesDto category)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\nCATEGORIA: {category.CategoryID}");
            Console.ResetColor();
            Console.WriteLine($"CategoryID: {category.CategoryID}");
            Console.WriteLine($"Nome da Categoria: {category.CategoryName}");
            Console.WriteLine($"Descrição: {category.Description}");
        }

        static int getID()
        {
            Console.WriteLine("\n Digite o ID");
            string input = Console.ReadLine();
            int id;
            int.TryParse(input, out id);
            return id;
        }
    }
}
