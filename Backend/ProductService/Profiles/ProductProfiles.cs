using AutoMapper;
using Models;

namespace ProductService;

public class ProductProfiles:Profile
{
    public ProductProfiles()
    {
        CreateMap<AddProductDto, Product>().ReverseMap();
        CreateMap<AddProductImageDto, ProductImages>().ReverseMap();
    }

}
