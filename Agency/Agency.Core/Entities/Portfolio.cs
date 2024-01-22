using Agency.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Entities
{
    public  class Portfolio:BaseAudiTable
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string? ImageUrl { get; set; }

    }
}
