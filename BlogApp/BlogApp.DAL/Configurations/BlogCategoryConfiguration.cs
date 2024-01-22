using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Configurations
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.HasOne(c => c.Category).WithMany(ca => ca.BlogCategories).HasForeignKey(b=>b.CategoryId);
            builder.HasOne(o=>o.Blog).WithMany(c=>c.BlogCategories).HasForeignKey(n=>n.BlogId);
        }
    }
}
