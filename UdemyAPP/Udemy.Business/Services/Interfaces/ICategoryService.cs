using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Core.Entities;

namespace Udemy.Business.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryListItemDto>> GetAllAsync();
        Task<CategoryGetDto> GetByIdAsync(int id);
        Task<CategoryCreateDto> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryUpdateDto> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task DeleteAsync(int id);
    }
}
