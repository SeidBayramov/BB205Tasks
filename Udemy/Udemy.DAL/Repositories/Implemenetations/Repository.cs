using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Common;
using Udemy.DAL.Repositories.Interface;

namespace Udemy.DAL.Repositories.Implemenetations
{
    public class Repository<T> : IRepository<T> where T : BaseAudiTable, new()
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await Task.FromResult(query);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task DeleteAsync(int id)
        {
            Check(id)
        }
    }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool Check(int id)
        {
           
            if(Table.Any(x => x.Id == id && !x.IsDeleted))
            {
            return true;
            }
            return false;
            
        }
    }
}
