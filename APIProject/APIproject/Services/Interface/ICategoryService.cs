using APIproject.DTOs;
using APIproject.Entittes;

namespace APIproject.Services.Interface
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetAll();
        Task<Category> GetById(int id);
        Task Create(CreateCategoryDTO createCategoryDto);
        Task Update(int id, UpdateCategoryDTO updateCategoryDto);
        Task Delete(int id);
    }
}
