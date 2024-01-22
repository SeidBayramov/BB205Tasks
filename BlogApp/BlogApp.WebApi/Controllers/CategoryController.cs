using BlogApp.Business.DTOs.Category;
using BlogApp.Business.Services.Interface;
using BlogApp.WebBusiness.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly IValidator<CategoryCreateDto> _validator;


        public CategoryController(ICategoryService categoryService, IValidator<CategoryCreateDto> validator)
        {
            _categoryService = categoryService;
            _validator = validator;
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
            return StatusCode(200,category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto createCategoryDto)
        {
            var createdCategory = await _categoryService.CreateAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromForm] CategoryUpdateDto updateCategoryDto)
        {
            var updatedCategory = await _categoryService.UpdateAsync(id, updateCategoryDto);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return Ok(); 
        }
    }
}
