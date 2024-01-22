using ExamAPP1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamAPP1.DAL.Configuration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
           builder.Property(x=>x.Title).IsRequired().HasMaxLength(50);
           builder.Property(x=>x.Description).IsRequired().HasMaxLength(10000000);
        }
    }
}
