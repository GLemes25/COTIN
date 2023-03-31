using NorthWind.Data.Logic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationNorthwind.Model
{
    public class ProductsDto 
    {
    public ProductsDto()
        {
            CategoriesDto category = new CategoriesDto();
            Category = category;
        }
    
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public CategoriesDto Category { get; set; }

        //public string QuantityPerUnit { get; set; }
        //public Nullable<decimal> UnitPrice { get; set; }
        //public Nullable<short> UnitsInStock { get; set; }
        //public Nullable<short> UnitsOnOrder { get; set; }
        //public Nullable<short> ReorderLevel { get; set; }
        //public bool Discontinued { get; set; }
    }

}