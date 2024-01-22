using Agency.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repository.Interface
{
    public interface IRepostiroy<T> where T : BaseAudiTable, new()
    {
        public DbSet<T> Table { get; }
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task CreateAsync(T entity);
        public void UpdateAsync(T entity);
        public void DeleteAsync(T entity);
        public Task<int> SaveChangesAsync();
    }
}
