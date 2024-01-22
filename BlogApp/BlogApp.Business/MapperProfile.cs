using AutoMapper;
using BlogApp.Business.DTOs.Category;
using BlogApp.WebBusiness.Entities;

namespace BlogApp.Business
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CategoryCreateDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.Logo));

            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.Logo));
        }
    }
}
