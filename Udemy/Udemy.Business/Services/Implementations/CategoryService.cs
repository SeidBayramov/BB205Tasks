using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.Api.DTOs.CategoryDtos;
using Udemy.Api.Entity;
using Udemy.Business.Services.Interface;
using Udemy.DAL.Repositories.Interface;

namespace Udemy.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IQueryable<Category>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            var category = await _repo.GetByIdAsync(categoryId);
            return category;
        }

        public async Task<Category> CreateAsync(CategoryCreateDto createCategoryDTO)
        {
            Category newCategory = new Category
            {
                Title = createCategoryDTO.Title,
                ParentCategoryId = createCategoryDTO.ParentCategoryId,
                CreatedAt = createCategoryDTO.CreatedAt,
                
            };

            var createdCategory = await _repo.CreateAsync(newCategory);
            return createdCategory;
        }

        public async Task<Category> UpdateAsync(CategoryUpdateDto updateCategoryDTO)
        {
            var existingCategory = await _repo.GetByIdAsync(updateCategoryDTO.Id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Title = updateCategoryDTO.Title;
            existingCategory.ParentCategoryId = updateCategoryDTO.ParentCategoryId;
            existingCategory.UpdatedAt = updateCategoryDTO.UpdatedAt;

            var updatedCategory = await _repo.UpdateAsync(existingCategory);
            return updatedCategory;
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Invalid ID");

            var category = await _repo.GetByIdAsync(id);

            if (category == null) throw new ArgumentException("Category not found");

            _repo.DeleteAsync(category);
            await _repo.SaveChangesAsync();
        }
    }
}
