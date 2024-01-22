using Priona.Core.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priona.Core.Entity
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public string SKU { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public bool IsDeleted { get; set; }

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public List<ProductTag>? ProductTags { get; set; }

        public List<ProductImages>? ProductImages { get; set; }

    }

}
