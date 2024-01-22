
using AutoMapper;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Core.Entities;

namespace Udemy.API
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryCreateDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryGetDto, Category>();
            CreateMap<CategoryListItemDto, Category>()
                .ForMember(x => x.ChildCategories, opt => opt.MapFrom(x => x.ChildCategories));
            CreateMap<Category, CategoryListItemDto>()
                .ForMember(x => x.ChildCategories, opt => opt.MapFrom(x => x.ChildCategories));
        }
    }
}
