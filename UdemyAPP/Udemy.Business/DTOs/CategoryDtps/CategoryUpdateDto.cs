using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.BaseDtos;

namespace Udemy.Business.DTOs.CategoryDtps
{
    public class CategoryUpdateDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
