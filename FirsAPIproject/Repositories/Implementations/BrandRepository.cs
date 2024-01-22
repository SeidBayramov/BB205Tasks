using FirsAPIproject.DAL;
using FirsAPIproject.Entites;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FirsAPIproject.Repositories.Implementations
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
