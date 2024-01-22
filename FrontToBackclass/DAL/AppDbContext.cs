using FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.DAL
{
    public class AppDbContext :DbContext
    {

        public AppDbContext( DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }


        public DbSet<Slider> sliders { get; set; }

        public DbSet<Card> cards { get; set; }
        public DbSet<Services> services { get; set; }

    }
}
