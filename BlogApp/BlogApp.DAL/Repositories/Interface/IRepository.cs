using BlogApp.Core.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogApp.DAL.Repositories.Interface
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }

        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes
        );

        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
