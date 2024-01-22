using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities;

namespace Udemy.DAL.Repositories.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> IsExistParentCategory(int? id);
        DbSet<Category> CategoryTable { get; }
    }
}
