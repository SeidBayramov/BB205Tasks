using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;

namespace Udemy.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        DbSet<T> Table { get; }
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression = null,
                Expression<Func<T, object>>? expressionOrderBy = null,
                bool isDescending = false,
                params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task<bool> IsExist(int id);
        Task SaveChangeAsync();
    }
}
