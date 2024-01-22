using Agency.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.DAL.Configuration
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(c=>c.Title).IsRequired().HasMaxLength(50);
            builder.Property(c => c.SubTitle).IsRequired().HasMaxLength(50);
            builder.Property(c => c.CreateAt).HasDefaultValueSql("getutcdate()");

        }
    }
}
