using Agency.Busines.Services.Interface;
using Agency.Busines.ViewModel.Portfolio;
using Agency.Core.Entities;
using Agency.DAL.Repository.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agency.Busines.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfol;
        private readonly IMapper _mapper;

        public PortfolioService(IPortfolioRepository portfolio, IMapper mapper)
        {
            _portfol = portfolio ?? throw new ArgumentNullException(nameof(portfolio));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateAsync(PortfolioCreateVm Vm)
        {
            var Portol = _mapper.Map<Portfolio>(Vm);

            await _portfol.CreateAsync(Portol);
            await _portfol.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var port = await _portfol.GetByIdAsync(id);

            _portfol.DeleteAsync(port);
            await _portfol.SaveChangesAsync();
        }

        public async Task<Portfolio> PortGetAsync(int id)
        {
            var port = await _portfol.GetByIdAsync(id) ?? throw new InvalidOperationException();

            return port;
        }

        public async Task<IEnumerable<Portfolio>> PortfolioGetAllAsync()
        {
            return await _portfol.GetAllAsync();
        }

        public async Task UpdateAsync(PortfolioUpdateVm Vm)
        {
            var port = _mapper.Map<Portfolio>(Vm);

            _portfol.UpdateAsync(port);
            await _portfol.SaveChangesAsync();
        }
    }
}
