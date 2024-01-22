using Carvilla.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carvilla.DAL.Configurations
{
	public class CarConfigurations : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Price).IsRequired();
			builder.Property(x => x.ImageUrl).IsRequired();
        }
    }
}
