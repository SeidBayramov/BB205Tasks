using AutoMapper;
using BlogApp.Business.DTOs.Category;
using BlogApp.Business.DTOs.Exceptions.Category;
using BlogApp.Business.DTOs.Exceptions.Common;
using BlogApp.Business.Helpers;
using BlogApp.Business.Services.Interface;
using BlogApp.DAL.Repositories;
using BlogApp.DAL.Repositories.Implemenetations;
using BlogApp.DAL.Repositories.Interface;
using BlogApp.WebBusiness.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public CategoryService(ICategoryRepository repo, IMapper mapper, IWebHostEnvironment environment)
        {
            _repo = repo;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IQueryable<Category>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new NegativeIdException();
            }
            var category = await _repo.GetByIdAsync(categoryId);

            if (category == null)
            {
                throw new CategoryNullException();
            }

            return category;
        }

        public async Task<Category> CreateAsync(CategoryCreateDto createCategoryDto)
        {
            if (!createCategoryDto.Logo.ContentType.Contains("image"))
            {
                throw new ValidationException("You can only upload image files.");
            }

            if (createCategoryDto.Logo.Length > 2097152) 
            {
                throw new ValidationException("You cannot upload an image larger than 2MB.");
            }

            string uploadedFilePath = FileManager.Upload(createCategoryDto.Logo, _environment.WebRootPath, @"\Upload\CategoryImage\");

            var category = _mapper.Map<Category>(createCategoryDto);

            category.LogoUrl = uploadedFilePath;

            return await _repo.CreateAsync(category);
        }

        public async Task<Category> UpdateAsync(int categoryId, CategoryUpdateDto updateCategoryDto)
        {
            if (categoryId <= 0)
            {
                throw new NegativeIdException();
            }

            var existingCategory = await _repo.GetByIdAsync(categoryId);

            if (existingCategory == null)
            {
                throw new CategoryNullException();
            }

            if (!string.IsNullOrEmpty(updateCategoryDto.Name))
            {
                existingCategory.Name = updateCategoryDto.Name;
            }

            if (updateCategoryDto.Logo != null)
            {
                string deletedFilePath = existingCategory.LogoUrl;

                FileManager.DeleteFile(deletedFilePath, _environment.WebRootPath, @"\Upload\CategoryImage\");

                existingCategory.LogoUrl = FileManager.Upload(updateCategoryDto.Logo, _environment.WebRootPath, @"\Upload\CategoryImage\");
            }

            return await _repo.UpdateAsync(existingCategory);
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException();

            var category = await _repo.GetByIdAsync(id);

            if (category == null) throw new CategoryNullException();

            _repo.DeleteAsync(category);
            await _repo.SaveChangesAsync();
        }
    }
}