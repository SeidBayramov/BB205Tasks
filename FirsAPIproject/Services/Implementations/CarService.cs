using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using FirsAPIproject.Services.Interfaces;

namespace FirsAPIproject.Services.Implementations
{
    public class CarService : ICarService
    {

        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IQueryable<Car>> GetAll()
        {
            return await _carRepository.GetAll();

        }

        public Task<Car> GetById(int id)
        {
            if (id == 0) throw new ArgumentException("Not Found");
            var car =_carRepository.GetByIdAsync(id);

            if (car == null) throw new ArgumentException("Not Found");
            return car;

        }

        public async Task Create(CreateCarDto createCarDto)
        {
            if (createCarDto==null) throw new ArgumentException("Not Found");

            Car car = new Car()
            {
                Name = createCarDto.Name,
            };
            await _carRepository.Create(car);
           
        }

    
    }
}
