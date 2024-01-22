using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;
using Udemy.DAL.Context;
using Udemy.DAL.Repositories.Interfaces;

namespace Udemy.DAL.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        private readonly AppDbContext _context;
        public DbSet<T> Table => _context.Set<T>();
        public Repository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<T>> GetAll(Expression<Func<T, bool>>? expression = null,
                Expression<Func<T, object>>? expressionOrderBy = null,
                bool isDescending = false,
                params string[] includes)
        {
            IQueryable<T> query = Table;
            if (expressionOrderBy != null)
            {
                query = isDescending ? query.OrderByDescending(expressionOrderBy) : query.OrderBy(expressionOrderBy);
            }
            if (includes is not null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query = query.Include(includes[i]);
                }
            }
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            return query.Where(x => !x.IsDeleted);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
        public async Task<T> Create(T entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            Table.Update(entity);
            return entity;
        }
        public async Task Delete(T entity)
        {
            entity.IsDeleted = true;
        }
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(int id)
        {
            return await Table.AnyAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}
