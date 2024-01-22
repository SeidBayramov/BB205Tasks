using App.CORE.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)       
            .WithMany(pc => pc.ChildCategories)     
            .HasForeignKey(c => c.ParentCategoryId) 
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Teacher> teachers { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<StudentsCourses> studentsCourses { get; set; }

    }
}
