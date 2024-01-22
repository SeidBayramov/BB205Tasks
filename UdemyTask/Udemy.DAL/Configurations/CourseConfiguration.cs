using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities;

namespace Udemy.DAL.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(t => t.Title).IsRequired().HasMaxLength(50);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(100);
        }
    }
}
