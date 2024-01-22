using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;

namespace FirsAPIproject.Repositories.Implementations
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        public CarRepository(AppDbContext context) : base(context)
        {
        }
    }
}
