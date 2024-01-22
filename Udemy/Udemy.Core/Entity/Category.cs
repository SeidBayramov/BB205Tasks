
using Udemy.Core.Common;

namespace Udemy.Api.Entity
{
    public class Category:BaseAudiTable
    {
        public string  Title { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
