using APIproject.Entittes.Base;
using System.Linq.Expressions;

namespace APIproject.Repositories.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>? orderexrepsion = null, params string[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();

    }
}
