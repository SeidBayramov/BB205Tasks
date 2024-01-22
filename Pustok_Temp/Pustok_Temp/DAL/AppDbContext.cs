using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pustok_Temp.Models;

namespace Pustok_Temp.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BookImages> BookImages { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
    }
}
