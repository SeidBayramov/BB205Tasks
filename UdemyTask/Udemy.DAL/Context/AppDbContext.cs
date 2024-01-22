using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities;

namespace Udemy.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Category> categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Category>()
            .HasOne(s => s.ParentCategory)
            .WithMany(m => m.ChildCategories)
            .HasForeignKey(e => e.ParentCategoryId);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentsCourses> studentsCourses { get; set; }
    }
}
