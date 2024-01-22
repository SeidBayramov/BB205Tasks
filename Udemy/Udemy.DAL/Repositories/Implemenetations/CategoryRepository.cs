using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Api.DAL.Context;
using Udemy.Api.Entity;
using Udemy.DAL.Repositories.Interface;

namespace Udemy.DAL.Repositories.Implemenetations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
