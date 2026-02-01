using AutoMapper;
using Repositories;

namespace Services.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();

        CreateMap<CreateProductRequest, Product>().ReverseMap().ForMember(destination => destination.Name,
            options => options.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<UpdateProductRequest, Product>().ReverseMap().ForMember(destination => destination.Name,
            options => options.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}