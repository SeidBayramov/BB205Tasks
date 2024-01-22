using APIproject.DAL;
using APIproject.Entittes.Base;
using APIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APIproject.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<IQueryable<T>> GetAll(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null,
            params string[] includes)
        {
            IQueryable<T> query = _table;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderExpression != null)
            {
                query = query.OrderBy(orderExpression);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> Create(T entity)
        {
            await _table.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
        }

        public void Delete(T entity)
        {
            _table.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
