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
    public class CarController : ControllerBase
    {
        private readonly IGenericRepository<Car> _carRepository;
        private readonly ICarService _service;

        public CarController(ICarRepository carRepository,ICarService service)
        {
            _carRepository = carRepository;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cars = await _service.GetAll();


            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById(int id)
        {
           

            var car = await _service.GetById(id);


            return Ok(car);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateCarDto car)
        {
          

            await _service.Create(car);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] Car car)
        {
            if (id <= 0 || car == null || car.Id != id)
                return BadRequest("Invalid id or car object");

            var existingCar = await _carRepository.GetByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            existingCar.BrandId = car.BrandId;
            existingCar.ColorId = car.ColorId;
            existingCar.ModelYear = car.ModelYear;
            existingCar.DailyPrice = car.DailyPrice;
            existingCar.Description = car.Description;

            _carRepository.Update(existingCar);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid id value");

            var existingCar = await _carRepository.GetByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            _carRepository.Delete(existingCar);
            await _carRepository.SaveChangesAsync();

            return Ok();
        }
    }
}
