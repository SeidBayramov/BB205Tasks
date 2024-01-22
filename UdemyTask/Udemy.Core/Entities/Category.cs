using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;

namespace Udemy.Core.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? ChildCategories { get; set; }
    }
}
