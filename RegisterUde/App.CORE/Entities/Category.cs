using App.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CORE.Entities
{
    public class Category:BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? ChildCategories { get; set; }




    }
}
