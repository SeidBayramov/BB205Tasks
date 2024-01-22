using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Api.DTOs.BaseDtos;

namespace Udemy.Business.DTOs.CategoryDtos
{
    public class CategoryListItemDto:BaseAuditableEntityTableDto
    {
        public string Name { get; set; }
    }
}
