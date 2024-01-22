using BlogApp.DAL.Context;
using BlogApp.DAL.Repositories.Interface;
using BlogApp.WebBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Implemenetations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
