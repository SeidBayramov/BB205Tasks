using Priona.Core.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priona.Core.Entity
{
    public class ProductTag : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
