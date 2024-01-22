using App.API.Helper;
using App.BUSINESS.DTOs.Brand;
using App.BUSINESS.DTOs.Category;
using App.BUSINESS.Services.Interfaces;
using App.CORE.Entities;
using App.DAL.Repositories.Implementations;
using App.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateCategoryDto createCategoryDto)
        {
            if (!createCategoryDto.LogoImg.CheckContent("image/"))
            {
                throw new Exception( "Duzgun format daxil edin");
            }
                if (createCategoryDto.ParentCategoryId != null)
                {
                    if (!_repo.Check((int)createCategoryDto.ParentCategoryId))
                    {
                        throw new Exception("Bele parent category movcud deyil");
                    }
                    Category category = new Category()
                    {
                        Name = createCategoryDto.Name,
                        LogoUrl = createCategoryDto.LogoImg.UploadFile(folderName: "C:\\Users\\User\\Desktop\\Repos\\Udemy\\App.BUSINESS\\Upload\\"),
                        ParentCategoryId=createCategoryDto.ParentCategoryId
                    };

                    await _repo.Create(category);
                    _repo.Save();

                }
            Category category2 = new Category()
            {
                Name = createCategoryDto.Name,
                LogoUrl = createCategoryDto.LogoImg.UploadFile(folderName: "C:\\Users\\User\\Desktop\\Repos\\Udemy\\App.BUSINESS\\Upload\\"),
            };

            await _repo.Create(category2);
            _repo.Save();

        }

        public async Task Delete(int id)
        {
             _repo.delete(id);
        }

        public async Task<ICollection<Category>> GetAllAsync()
        {
            var categories = await _repo.GetAllAsync();
            var ParentCategories=categories.Where(x=>x.ParentCategoryId==null );
             return await ParentCategories.ToListAsync(); 
        }

        public async Task<Category> GetById(int id)
        {
            if(id<=0) throw new Exception("Bad Request");
            return await _repo.GetById(id);
        }

        public async Task<ICollection<Category>> RecycleBin()
        {
            var categories = await _repo.RecycleBin();
            return await categories.ToListAsync();
        }

        public async Task Update(UpdateCategoryDto updateCategoryDto)
        {

            if (updateCategoryDto == null) throw new Exception("Bad Request");
                var existingCategory = await _repo.GetById(updateCategoryDto.Id);
            if (updateCategoryDto.ParentCategoryId != null)
            {
                if (!_repo.Check((int)updateCategoryDto.ParentCategoryId))
                {
                    throw new Exception("Bele parent category movcud deyil");
                }
              
                existingCategory.ParentCategoryId = updateCategoryDto.ParentCategoryId;

            }
            existingCategory.Name = updateCategoryDto.Name;
            if (updateCategoryDto.LogoImg != null)
            {
                existingCategory.LogoUrl.RemoveFile("C:\\Users\\II novbe\\Desktop\\New folder (4)\\Udemy\\App.BUSINESS\\Upload\\");
                existingCategory.LogoUrl = updateCategoryDto.LogoImg.UploadFile(folderName: "C:\\Users\\II novbe\\Desktop\\New folder (4)\\Udemy\\App.BUSINESS\\Upload\\");
            }

            _repo.Update(existingCategory);
                _repo.Save();
            
        }
        public async Task Restore()
        {
            _repo.Restore();
            _repo.Save();
        }

        public async Task deleteAll()
        {
            _repo.deleteAll();
            _repo.Save();
        }
    }
}
