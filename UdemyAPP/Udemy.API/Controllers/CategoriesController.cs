using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Business.Exceptions.Category;
using Udemy.Business.Exceptions.Common;
using Udemy.Business.Services.Interfaces;
using Udemy.Core.Entities;

namespace Udemy.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _catService;
        private readonly IWebHostEnvironment _env;
        public CategoriesController(ICategoryService catService, IWebHostEnvironment env)
        {
            _catService = catService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            ICollection<CategoryListItemDto> categories = await _catService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await _catService.GetByIdAsync(id);
                return StatusCode(StatusCodes.Status200OK, category);
            }
            catch (NegativeIdException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDto catDto)
        {
            try
            {
                var category = await _catService.CreateAsync(catDto);
                return StatusCode(StatusCodes.Status201Created, category);
            }
            catch (CategoryNullException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto catDto)
        {
            try
            {
                var category = await _catService.UpdateAsync(catDto);
                return StatusCode(StatusCodes.Status200OK, category);
            }
            catch (NegativeIdException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _catService.DeleteAsync(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (NegativeIdException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (CategoryNotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
    }
}
