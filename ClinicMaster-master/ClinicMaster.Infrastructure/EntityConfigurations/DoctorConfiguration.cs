using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(d => d.PhysicianId).IsRequired();
            builder.Property(d => d.SpecializationId).IsRequired();
            builder.Property(d => d.Name).IsRequired().HasMaxLength(255);
            builder.Property(d => d.Phone).IsRequired();
        }
    }
}
