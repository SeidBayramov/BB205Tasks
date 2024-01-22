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
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.Property(n => n.Name).IsRequired().HasMaxLength(30);
            builder.Property(s => s.Surname).IsRequired().HasMaxLength(60);
            builder.Property(a => a.Age).IsRequired();
        }
    }
}
