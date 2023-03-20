using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    internal class Program
    {
        static void Main(string[] args)
        {

           
            var arrayList = new ArrayList
                    {
                        "Gabriel",
                        10,
                        true,
                        5.10
                    };
            arrayList.Add(DateTime.Now);
            Console.WriteLine(arrayList.Count);

            foreach (var item in arrayList)
            {
                Console.WriteLine("{0} => {1} {2}", item, item.GetType());
            }
            Console.ReadKey();
        }
    }
}
