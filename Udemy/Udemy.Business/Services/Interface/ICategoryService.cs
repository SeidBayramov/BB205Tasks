using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Api.DTOs.CategoryDtos;
using Udemy.Api.Entity;

namespace Udemy.Business.Services.Interface
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int categoryId);
        Task<Category> CreateAsync(CategoryCreateDto createCategoryDTO);
        Task<Category> UpdateAsync(CategoryUpdateDto updateCategoryDTO);
        Task DeleteAsync(int id);
    }
}
