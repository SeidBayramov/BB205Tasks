using APIproject.Entittes;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIproject.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}