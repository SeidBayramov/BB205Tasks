using FirsAPIproject.DAL;
using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using FirsAPIproject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirsAPIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IGenericRepository<Brand> _brandrepo;
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService, IBrandRepository brandRepo)
        {
            _brandService = brandService;
            _brandrepo = brandRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _brandService.GetAll();
            return StatusCode(StatusCodes.Status200OK, brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
         

            var brand = await _brandService.GetById(id);

      

            return Ok(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandDto brandDto)
        {


            await _brandService.Create(brandDto);
            await _brandrepo.SaveChangesAsync(); 
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Brand brand)
        {
            if (id <= 0 || brand == null || brand.Id != id)
                return BadRequest("Invalid id or brand object");

            var existingBrand = await _brandrepo.GetByIdAsync(id);
            if (existingBrand == null)
                return NotFound();

            existingBrand.Name = brand.Name;

            _brandrepo.Update(existingBrand);
            await _brandrepo.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id value");

            var existingBrand = await _brandrepo.GetByIdAsync(id);
            if (existingBrand == null)
                return NotFound();

            _brandrepo.Delete(existingBrand);
            await _brandrepo.SaveChangesAsync();
            return Ok();
        }
    }
}
