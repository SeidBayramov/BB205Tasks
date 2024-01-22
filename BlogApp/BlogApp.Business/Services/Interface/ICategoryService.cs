using BlogApp.Business.DTOs.Category;
using BlogApp.WebBusiness.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Interface
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int categoryId);
        Task<Category> CreateAsync(CategoryCreateDto createCategoryDTO);
        Task<Category> UpdateAsync(int categoryId, CategoryUpdateDto updateCategoryDTO);
        Task DeleteAsync(int id);
    }
}
