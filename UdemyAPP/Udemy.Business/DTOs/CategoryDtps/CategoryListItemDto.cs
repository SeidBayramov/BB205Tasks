using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.BaseDtos;

namespace Udemy.Business.DTOs.CategoryDtps
{
    public class CategoryListItemDto : BaseAuditableEntityDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public CategoryListItemDto? ParentCategory { get; set; }
        public ICollection<CategoryListItemDto>? ChildCategories { get; set; }
    }
}
