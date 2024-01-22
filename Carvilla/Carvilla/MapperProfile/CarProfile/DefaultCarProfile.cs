using AutoMapper;
using Carvilla.Areas.Manage.ViewModels;
using Carvilla.Models;

namespace Carvilla.MapperProfile.CarProfile
{
    public class DefaultCarProfile : Profile
    {
        public DefaultCarProfile()
        {
            CreateMap<CarCreateVm, Car>();
            CreateMap<CarUpdateVm, Car>();
            CreateMap<CarUpdateVm, Car>().ReverseMap();
        }
    }
}
