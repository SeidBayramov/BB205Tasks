using Agency.Core.Entities;
using Agency.DAL.Context;
using Agency.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repository.Implementations
{
    public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
