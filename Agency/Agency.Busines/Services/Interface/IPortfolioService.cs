using Agency.Busines.ViewModel.Portfolio;
using Agency.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.Services.Interface
{
    public interface IPortfolioService 
    {
        public Task<IEnumerable<Portfolio>> PortfolioGetAllAsync();
        public Task<Portfolio> PortGetAsync(int id);
        Task CreateAsync(PortfolioCreateVm Vm);
        Task UpdateAsync(PortfolioUpdateVm Vm);
        Task DeleteAsync(int id);
    }
}
