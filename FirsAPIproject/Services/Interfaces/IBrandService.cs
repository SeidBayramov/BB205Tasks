using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;
using System.Linq;
using System.Threading.Tasks;

namespace FirsAPIproject.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IQueryable<Brand>> GetAll();
        Task<Brand> GetById(int id);
        Task Create(CreateBrandDto createBrand);

    }
}