using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebSite.Models;

namespace WebSite.DAL
{
    namespace StoriesApp.DAL
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Recipes> Recipes { get; set; }
            public DbSet<Stories> Stories { get; set; }
        }
    }
}