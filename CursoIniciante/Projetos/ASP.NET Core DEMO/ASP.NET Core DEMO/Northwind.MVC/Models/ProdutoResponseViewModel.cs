using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.MVC.Models
{
    public class ProdutoResponseViewModel
    {
        public bool success { get; set; }
        public List<Products> Data { get; set; }

    }
}
