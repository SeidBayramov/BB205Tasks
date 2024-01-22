using APIproject.DTOs;
using APIproject.Entittes;
using APIproject.Repositories.Interface;
using APIproject.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateCategoryDTO car)
        {

            await _categoryService.Create(car);
            await _categoryRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm]UpdateCategoryDTO categoryDto)
        {
      
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
          
            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;
            existingCategory.Tag=categoryDto.Tag;

            _categoryRepository.Update(existingCategory);
            await _categoryRepository.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
        
            _categoryRepository.Delete(existingCategory);
            await _categoryRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
