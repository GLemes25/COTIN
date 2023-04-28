using AutoMapper;
using Northwind.API.Logic.Model;
using Northwind.Data.Logic.Data.Northwind.Entity;

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
