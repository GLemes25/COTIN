using AutoMapper;
using Northwind.Data.Logic.Data.Northwind.Entity;
using Northwind.MVC.Models;

namespace Northwind.MVC.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<ProductsViewModel, Products>().ReverseMap();
        }
    }
}
