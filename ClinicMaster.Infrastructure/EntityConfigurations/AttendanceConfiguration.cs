﻿using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
    {
        public void Configure(EntityTypeBuilder<Attendance> builder)
        {
            builder.Property(p => p.PatientId).IsRequired();
            // builder.Property(p => p.ClinicRemarks).IsRequired();
            builder.Property(p => p.Diagnosis).IsRequired().HasMaxLength(255);
            //  builder.Property(p => p.Therapy).IsRequired();
        }
    }
}
