using Agency.Core.Common;
using Agency.DAL.Context;
using Agency.DAL.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Repository.Implementations
{
    public class Repository<T> : IRepostiroy<T> where T : BaseAudiTable, new()
    {

        private readonly AppDbContext _context;
        public DbSet<T> Table => _context.Set<T>();

        public Repository(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IQueryable<T> entities = Table.Where(e => !e.IsDeleted);
            return await entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T entity = await Table.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }


        public async Task CreateAsync(T entity)
        {
          await Table.AddAsync(entity);
        }

        public async void DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.UpdateAt = DateTime.Now;
            Table.Update(entity);
        }

      
        public  async Task<int> SaveChangesAsync()
        {
            var res = await _context.SaveChangesAsync();
            return res;
        }

        public async void UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
