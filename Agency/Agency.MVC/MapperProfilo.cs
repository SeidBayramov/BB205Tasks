using Agency.Busines.ViewModel.Portfolio;
using Agency.Core.Entities;
using AutoMapper;

namespace Agency.MVC
{
    public class MapperProfilo:Profile
    {
        public MapperProfilo()
        {
        CreateMap<PortfolioCreateVm, Portfolio>();
        CreateMap<PortfolioUpdateVm, Portfolio>();
        CreateMap<PortfolioCreateVm, Portfolio>().ReverseMap();
        CreateMap<PortfolioUpdateVm, Portfolio>().ReverseMap();

        }

    }
}
