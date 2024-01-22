using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy.Api.DTOs.CategoryDtos;
using Udemy.Business.Services.Interface;

namespace Udemy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //private readonly IValidator<CreateCategoryDto> _validator;
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto createCategoryDto)
        {
            var createdCategory = await _categoryService.CreateAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto updateCategoryDto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(updateCategoryDto.Id, updateCategoryDto);

            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);



            return NoContent();
        }
    }
}
