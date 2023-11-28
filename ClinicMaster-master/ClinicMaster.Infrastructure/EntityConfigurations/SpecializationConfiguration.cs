using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.Property(s => s.Name).IsRequired().HasMaxLength(255);

            //seed Data
            builder.HasData(
               new Specialization { Id = 1, Name = "Dentistry" },
               new Specialization { Id = 2, Name = "Ophthalmology" }
            );
        }
    }
}
