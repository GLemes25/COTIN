using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleApp1.Logic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var UfService= new UfService();
            Dictionary<string, string> UfToAdd = new Dictionary<string, string>();
            UfToAdd.Add("test", "test");
            UfService.PersistUf(UfToAdd);
            var ufList = UfService.GetUf("");

            foreach (var uf in ufList)
            {
                Console.WriteLine();
                Console.WriteLine(uf.SiglaID);
                Console.WriteLine(uf.Nome);
            }
    
            Console.ReadKey();
        }

    }
}
