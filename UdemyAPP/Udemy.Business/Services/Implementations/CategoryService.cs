using AutoMapper;
using Microsoft.Data.SqlClient.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Business.Exceptions.Category;
using Udemy.Business.Exceptions.Common;
using Udemy.Business.Services.Interfaces;
using Udemy.Core.Entities;
using Udemy.DAL.Repositories.Interfaces;

namespace Udemy.Business.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _catRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository;
            _mapper = mapper;
        }

        public async Task<CategoryCreateDto> CreateAsync(CategoryCreateDto catDto)
        {
            if (catDto == null) throw new CategoryNullException();
            Category category = _mapper.Map<Category>(catDto);
            category.CreatedAt = DateTime.Now;

            await CheckParentCategory(catDto.ParentCategoryId);
            await _catRepository.Create(category);

            await _catRepository.SaveChangeAsync();
            return _mapper.Map<CategoryCreateDto>(category);
        }
        public async Task<CategoryUpdateDto> UpdateAsync(CategoryUpdateDto catDto)
        {
            await CheckEntity(catDto.Id);

            Category category = await _catRepository.GetByIdAsync(catDto.Id);
            _mapper.Map(catDto, category);
            await _catRepository.Update(category);
            category.UpdatedAt = DateTime.Now;

            await _catRepository.SaveChangeAsync();
            return _mapper.Map<CategoryUpdateDto>(category);
        }
        public async Task DeleteAsync(int id)
        {
            await CheckEntity(id);
            Category category = await _catRepository.GetByIdAsync(id);

            await _catRepository.Delete(category);
            await _catRepository.SaveChangeAsync();
        }

        public async Task<ICollection<CategoryListItemDto>> GetAllAsync()
        {
            var categories = await _catRepository.GetAll();
            return _mapper.Map<ICollection<CategoryListItemDto>>(categories);
        }

        public async Task<CategoryGetDto> GetByIdAsync(int id)
        {
            await CheckEntity(id);
            return _mapper.Map<CategoryGetDto>(await _catRepository.GetByIdAsync(id));
        }
        public async Task<bool> CheckEntity(int id)
        {
            if (id <= 0) throw new NegativeIdException();
            if(!await _catRepository.IsExist(id)) throw new CategoryNotFoundException();
            return true;
        }
        public async Task<bool> CheckParentCategory(int? id)
        {
            if (id == null) return true;
            if (id <= 0) throw new NegativeIdException();
            if(!await _catRepository.IsExistParentCategory(id)) throw new CategoryNotFoundException();
            return true;
        }
    }
}
