using JofExam.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JofExam.DAL.Configuration
{
    public class FruitConfiguration : IEntityTypeConfiguration<Fruit>
    {
        public void Configure(EntityTypeBuilder<Fruit> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.SubTitle).IsRequired().HasMaxLength(50);

        }
    }
}
