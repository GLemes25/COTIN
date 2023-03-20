using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Logic
{
    internal class CustomersService
    {
        private List<Customers> lista = new List<Customers>();
        public CustomersService()
        {
            CarregarListadeCustomers();
        }
        public void InserirCustomers(Customers customers)
        {
            lista.Add(customers);
        }
        public void RemoverCustomers(Customers customers)
        {
            lista.Remove(customers);
        }

        public List<Customers> GetCustomers(string CompanyName)
        {
            return lista.Where(x => x.CompanyName.Contains(CompanyName)|| x.CustomersID.Contains(CompanyName)).ToList();
        }
        public List<Customers> ObterListadeCustomers()
        {
            return lista;
        }
        private void CarregarListadeCustomers()
        {
            var cust1 = new Customers() { CustomersID = "ALFKI", CompanyName = "Bólido Comidas preparadas", ContactName = "Soft drinks, coffees, teas, beers, and ales", ContactTitle = "Sales Representative ", Address = "Obere Str. 57", City = "Berlin ", Region = "NULL", PostalCode = "12209 ", Country = "Germany ", Phone = "030-0074321 ", Fax = "030-0076545 " };
            var cust2 = new Customers() { CustomersID = "ANATR", CompanyName = "Alfreds Futterkiste", ContactName = "Sweet and savory sauces, relishes, spreads, and seasonings", ContactTitle = "Owner ", Address = "Avda. de la Constitución 2222 ", City = "México D.F. ", Region = "NULL", PostalCode = "05021", Country = "Mexico ", Phone = "(5) 555-4729 ", Fax = "(5) 555-3745 " };
            var cust3 = new Customers() { CustomersID = "ANTON", CompanyName = "Ana Trujillo Emparedados y helados", ContactName = "Desserts, candies, and sweet breads", ContactTitle = " Owner", Address = "Mataderos  2312", City = "México D.F. ", Region = "NULL", PostalCode = "05023 ", Country = "Mexico ", Phone = "(5) 555-3932 ", Fax = "NULL " };
            var cust4 = new Customers() { CustomersID = "AROUT", CompanyName = "Antonio Moreno Taquería", ContactName = "Cheeses", ContactTitle = "Sales Representative", Address = "120 Hanover Sq", City = "London ", Region = "NULL", PostalCode = "WA1 1DP ", Country = "UK ", Phone = "(171) 555-7788 ", Fax = "(171) 555-6750 " };
            var cust5 = new Customers() { CustomersID = "BERGS", CompanyName = "Around the Horn", ContactName = "Breads, crackers, pasta, and cereal", ContactTitle = "Order Administrator ", Address = "Berguvsvägen  8 ", City = "Luleå ", Region = "NULL", PostalCode = "S-958 22 ", Country = "Sweden ", Phone = "0921-12 34 65 ", Fax = "0921-12 34 67 " };
            var cust6 = new Customers() { CustomersID = "BLAUS", CompanyName = "Berglunds snabbköp", ContactName = "Prepared meats", ContactTitle = "Sales Representative ", Address = "Forsterstr. 57", City = "Mannheim ", Region = "NULL", PostalCode = "68306", Country = "Germany ", Phone = "0621-08460 ", Fax = "0621-08924 " };
            var cust7 = new Customers() { CustomersID = "BLONP", CompanyName = "Blauer See Delikatessen", ContactName = "Dried fruit and bean curd", ContactTitle = "Marketing Manager ", Address = "24, place Kléber", City = "Strasbourg", Region = "NULL", PostalCode = "67000", Country = "France ", Phone = "88.60.15.31", Fax = "88.60.15.32" };
            var cust8 = new Customers() { CustomersID = "BOLID", CompanyName = "Blondesddsl père et fils", ContactName = "Seaweed and fish", ContactTitle = "Owner ", Address = "C/ Araquil, 67 ", City = "Madrid ", Region = "NULL", PostalCode = "28023 ", Country = "Spain ", Phone = "(91) 555 22 82 ", Fax = "(91) 555 91 99 " };

            lista.Add(cust2);
            lista.Add(cust3);
            lista.Add(cust4);
            lista.Add(cust5);
            lista.Add(cust6);
            lista.Add(cust7);
            lista.Add(cust8);
        }
    }
}
