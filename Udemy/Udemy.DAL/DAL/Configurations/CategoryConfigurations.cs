using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Udemy.Api.Entity;

namespace Udemy.Api.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(65);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.ChildCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.ChildCategories)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
