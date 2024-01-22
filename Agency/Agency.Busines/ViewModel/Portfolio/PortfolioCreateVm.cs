using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Busines.ViewModel.Portfolio
{
    public class PortfolioCreateVm
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
