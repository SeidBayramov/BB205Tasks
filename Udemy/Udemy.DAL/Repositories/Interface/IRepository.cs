using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Common;

namespace Udemy.DAL.Repositories.Interface
{
    public interface IRepository<T> where T : BaseAudiTable, new()
    {
        DbSet<T> Table { get; }

        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes
        );

        Task<T> GetByIdAsync(int id);
        bool Check(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int Id);
        Task SaveChangesAsync();
    }
}
