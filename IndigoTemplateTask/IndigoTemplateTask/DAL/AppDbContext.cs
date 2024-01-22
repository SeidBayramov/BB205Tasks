using IndigoTemplateTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IndigoTemplateTask.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }


    }

}