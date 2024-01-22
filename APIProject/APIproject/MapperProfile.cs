using APIproject.DTOs;
using APIproject.Entittes;
using AutoMapper;

namespace APIproject
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CreateCategoryDTO>();
            CreateMap<CreateCategoryDTO, Category>();

            CreateMap<UpdateCategoryDTO, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
                

            CreateMap<Category, UpdateCategoryDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag));
        }
    }
}
