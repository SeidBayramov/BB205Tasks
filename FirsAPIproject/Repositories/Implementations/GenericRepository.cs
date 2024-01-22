using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FirsAPIproject.DAL;
using FirsAPIproject.Entities.Base;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FirsAPIproject.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public GenericRepository(AppDbContext context)
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
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
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
