using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Udemy.Api.Entity;

namespace Udemy.Api.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }



    }
}
