using Northwind.Data.Logic.Data.Northwind.Entity;

namespace Northwind.MVC.Models
{
    public class ProductsResponseViewModel
    {
        public bool success { get; set; }
        public ProductsViewModel Data { get; set; }

    }
}
