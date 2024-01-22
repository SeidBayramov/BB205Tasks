using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;

namespace FirsAPIproject.Services.Interfaces
{
    public interface ICarService
    {
        Task<IQueryable<Car>> GetAll();
        Task<Car> GetById(int id);
        Task Create(CreateCarDto createCarDto);
    }
}
