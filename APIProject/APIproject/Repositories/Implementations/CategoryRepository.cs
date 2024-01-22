using APIproject.DAL;
using APIproject.Entittes;
using APIproject.Repositories.Interface;

namespace APIproject.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
