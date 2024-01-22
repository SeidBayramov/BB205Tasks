using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pustok_Temp.Models;

namespace Pustok_Temp.DAL
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Book_Img> bookimages { get; set; }
        public DbSet<Categories> categories { get; set; }
        public DbSet<Slider> sliders { get; set; }

        public DbSet<ParentCategory> parentcats { get; set; }


    }
}
