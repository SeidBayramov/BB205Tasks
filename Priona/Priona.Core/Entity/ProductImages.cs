using Priona.Core.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priona.Core.Entity
{
    public class ProductImages:BaseEntity
    {
        public string ImgUrl { get; set; }
        public bool? IsPrime { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
