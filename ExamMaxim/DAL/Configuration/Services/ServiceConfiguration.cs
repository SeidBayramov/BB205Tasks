using ExamMaxim.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamMaxim.DAL.Configuration.Services
{
    public class ServiceConfiguration:IEntityTypeConfiguration<Service>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Description).IsRequired().HasMaxLength(2000);
        }
    }
}
