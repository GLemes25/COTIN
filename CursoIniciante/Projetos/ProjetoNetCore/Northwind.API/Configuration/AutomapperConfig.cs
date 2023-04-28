using AutoMapper;
using Northwind.API.Logic.Model;
using NorthWind.Data.Logic.Data.NorthWind.Entity;

namespace Northwind.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<CategoriesViewModel, Categories>().ReverseMap();
            CreateMap<ProductsViewModel, Products>().ReverseMap();
            CreateMap<SuppliersViewModel, Suppliers>().ReverseMap();
        }
    }
}
