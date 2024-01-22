using FirsAPIproject.DTOs.BrandDtos;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Implementations;
using FirsAPIproject.Repositories.Interface;
using FirsAPIproject.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FirsAPIproject.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task Create(CreateBrandDto createBrand)
        {
            if (createBrand == null) throw new ArgumentException("Not Found");

            Brand brand = new Brand()
            {
                Name = createBrand.Name,
            };
            await _brandRepository.Create(brand);

        }

        public async Task<IQueryable<Brand>> GetAll()
        {
            return await _brandRepository.GetAll();
        }

        public async Task<Brand> GetById(int id)
        {
            if (id == 0) throw new ArgumentException("Not Found");
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null) throw new ArgumentException("Not Found");


            return brand;
        }
    }
}