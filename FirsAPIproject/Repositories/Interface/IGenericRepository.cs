using FirsAPIproject.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FirsAPIproject.Repositories.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression = null , Expression<Func<T, object>>? orderexrepsion = null, params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
