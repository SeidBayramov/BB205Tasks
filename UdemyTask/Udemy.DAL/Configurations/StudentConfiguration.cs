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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(n => n.Name).IsRequired().HasMaxLength(30);
            builder.Property(s => s.Surname).IsRequired().HasMaxLength(60);
            builder.Property(a => a.Age).IsRequired();
        }
    }
}
