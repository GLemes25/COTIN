using Northwind.Business.Logic.Validation;
using NorthWind.Data.Logic.Data;
using NovoConsole.Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApi.Models;

namespace NovoConsole
{
    internal class Program
    {
        public static OrdersService OrdersService { get; private set; }

        static void Main(string[] args)
        {
            OrdersService = new OrdersService();

            int option = 0;
            while (option != 6)
            {

                Console.WriteLine("\n==============================================================================");
                Console.WriteLine("\n\tEscolha uma opção:");
                Console.WriteLine("\t1 - GET: Obter Lista de Orders");
                Console.WriteLine("\t2 - GET: Obter Order por ID");
                Console.WriteLine("\t3 - GET: Obter Order por Nome");
                Console.WriteLine("\t4 - POST: Adicionar Order");
                Console.WriteLine("\t5 - PUT: Alterar Order");
                Console.WriteLine("\t6 - DELETE: Excluir Order");
                Console.WriteLine("\t7 - Sair\n");
                Console.WriteLine("==============================================================================");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Opção inválida, tente novamente.");
                    continue;
                }

                switch (option)
                {
                    case 1:

                        ordersListBypage();
                        break;
                    case 2:
                        OrderByID();
                        break;
                    case 3:
                        OrdersByName();
                        break;
                    case 4:
                        AdicionarOrder();
                        break;
                    case 5:
                        AlterarOrder();
                        break;
                    case 6:
                        DeletarOrder();
                        break;
                    case 7:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida, tente novamente.");
                        break;


                }
                Console.ReadKey();
                Console.Clear();
            }

            Console.ReadKey();
        }

        static void OrderByID()
        {
            var order = OrdersService.GetOrdersByID(getID());
            showOrders(order);
        }
        static void OrdersByName()
        {
            Console.WriteLine("\n Digite o Nome");
            string name = Console.ReadLine();
            var orders = OrdersService.GetOrdersByName(name);
            int count = 1;
            foreach (var order in orders)
            {


                showOrders(order, count);
                count++;

            }
        }

        //static void OrdersListByPage(int pageNo = 1, int pageSize = 20)
        //{


        //    var orders = OrdersService.GetOrdersListByPage(pageNo, pageSize);
        //    foreach (var order in orders)
        //    {


        //        showOrders(order);

        //    }

        //}

        //static void OrdersList()
        //{


        //    var orders = OrdersService.GetOrdersList();
        //    foreach (var order in orders)
        //    {


        //        showOrders(order);

        //    }

        //}

        static void ordersListBypage()
        {


            var allOrders = OrdersService.GetOrdersList();

            Console.ForegroundColor = ConsoleColor.DarkBlue;


            Console.WriteLine($"\tFoi encontrado {allOrders.Count} Order");
            Console.ResetColor();

            Console.WriteLine("Digite a quantidade de itens por paginas");
            string getInputByPage = Console.ReadLine();
            int pageSize;
            int.TryParse(getInputByPage, out pageSize);

            int pageNo = 1;

            while (true)
            {
                Console.WriteLine("\n===============================================");
                Console.WriteLine("\tpágina " + (pageNo));
                Console.WriteLine("===============================================");


                var orders = OrdersService.GetOrdersListByPage(pageNo, pageSize);

                int counter = pageSize * (pageNo-1);

                foreach (var order in orders)
                {


                    showOrders(order, counter);
                    counter++;
                }

                Console.WriteLine("\npressione 'd' para próxima página ou 'a' para página anterior ou a pagina. digite qualquer outra tecla para sair.");

                string option = Console.ReadLine();
                int page;
                //verifica se é d ou d 
                if (option.Equals("d", StringComparison.OrdinalIgnoreCase))
                {
                    pageNo++;
                    if ((pageNo-1) * pageSize >= allOrders.Count)
                    {
                        Console.WriteLine("você já está na última página!");
                        pageNo--;
                        Console.ReadKey();

                    }
                }
                else if (option.Equals("a", StringComparison.OrdinalIgnoreCase))
                {
                    pageNo--;
                    if (pageNo < 1)
                    {
                        Console.WriteLine("você já está na primeira página!");
                        pageNo++;
                        Console.ReadKey();

                    }
                }
                else if (int.TryParse(option, out page))
                {
                    pageNo = page ;
                    if ((pageNo - 1) * pageSize >= allOrders.Count)

                    {
                        Console.WriteLine("você já está na última página!");
                        Console.ReadKey();


                        pageNo = (allOrders.Count / pageSize);
                    }
                    if (pageNo < 1)
                    {
                        Console.WriteLine("você já está na primeira página!");
                        Console.ReadKey();


                        pageNo = 1
                            ;

                    }
                }
                else
                {
                    break;
                }


            }
        }
        static void AdicionarOrder()
        {
            int id = getID();
            var order = OrdersService.GetOrdersByID(10248);

            order.OrderID = id;
            order.ShipName = "Test";
            order.ShipAddress = "Test";
            order.ShipCity = "Test";
            order.ShipCountry = "Test";

            var orderAdd = OrdersService.AddOrders(order);
            showOrders(orderAdd);

        }
        static void AlterarOrder()
        {
            var id = getID();

            var order = OrdersService.GetOrdersByID(id);

            if (order != null)
            {


                order.OrderID = id;
                order.ShipName = "a";
                order.ShipAddress = "a";
                order.ShipCity = "a";
                order.ShipCountry = "a";

                var newOrder = OrdersService.UpdateOrders(id, order);

                showOrders(newOrder);
            }
        }

        static void DeletarOrder()
        {
            var deleted = OrdersService.DeleteOrdersID(getID());

            string resultado = deleted ? "Order Deletada" : "Erro: Não foi possivel deletar categoria";
            Console.WriteLine(resultado);
        }

        static void showOrders(OrderDto order, int? counter = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n\nORDER ");
            if (counter != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(counter + 1);
                Console.ResetColor();
            }
            else { Console.WriteLine(); }
            Console.ResetColor();
            Console.WriteLine($"OrderID: {order.OrderID}");
            Console.WriteLine($"CustomerID: {order.CustomerID}");
            Console.WriteLine($"OrderDate: {order.OrderDate}");
            Console.WriteLine($"EmployeeID: {order.EmployeeID}");
            Console.WriteLine($"RequiredDate: {order.RequiredDate}");
            Console.WriteLine($"ShippedDate: {order.ShippedDate}");
            Console.WriteLine($"ShipVia: {order.ShipVia}");
            Console.WriteLine($"Freight: {order.Freight}");
            Console.WriteLine($"ShipName: {order.ShipName}");
            Console.WriteLine($"ShipAddress: {order.ShipAddress}");
            Console.WriteLine($"ShipCity: {order.ShipCity}");
            Console.WriteLine($"ShipRegion: {order.ShipRegion}");
            Console.WriteLine($"ShipPostalCode: {order.ShipPostalCode}");
            Console.WriteLine($"ShipCountry: {order.ShipCountry}");
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
