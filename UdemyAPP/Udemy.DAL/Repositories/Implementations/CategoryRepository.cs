using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities;
using Udemy.DAL.Context;
using Udemy.DAL.Repositories.Interfaces;

namespace Udemy.DAL.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public DbSet<Category> CategoryTable => _context.Set<Category>();
        public async Task<bool> IsExistParentCategory(int? id)
        {
            return await CategoryTable.AnyAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
