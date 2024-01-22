using Azure;
using DianaTemp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DianaTemp.DAL
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Size> Sizes { get; set; }
		public DbSet<Colour> Colours { get; set; }
		public DbSet<Material> Materials { get; set; }
		public DbSet<ProductSize> ProductSizes { get; set; }
		public DbSet<ProductMaterial> ProductMaterials { get; set; }
		public DbSet<ProductColour> ProductColours { get; set; }

		public DbSet<Setting> Settings { get; set; }
		public DbSet<ProductImages> ProductImages { get; set; }


	}
}
