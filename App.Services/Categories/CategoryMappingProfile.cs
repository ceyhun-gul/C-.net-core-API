using AutoMapper;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;

namespace Services.Categories;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();

        CreateMap<Category, CategoryWithProductsDto>().ReverseMap();

        CreateMap<CreateCategoryRequest, Category>().ReverseMap().ForMember(destination => destination.Name,
            options => options.MapFrom(src => src.Name.ToLowerInvariant()));

        CreateMap<UpdateCategoryRequest, Category>().ReverseMap().ForMember(destination => destination.Name,
            options => options.MapFrom(src => src.Name.ToLowerInvariant()));
    }
}