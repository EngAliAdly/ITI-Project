using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.PatientId).IsRequired();
            builder.Property(a => a.DoctorId).IsRequired();
            builder.Property(a => a.StartDateTime).IsRequired();
            builder.Property(a => a.Detail).IsRequired();
            builder.Property(a => a.Status).IsRequired();
        }
    }
}
