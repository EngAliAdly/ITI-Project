using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class AssistantConfiguration : IEntityTypeConfiguration<Assistant>
    {
        public void Configure(EntityTypeBuilder<Assistant> builder)
        {
            builder.Property(d => d.Name).IsRequired().HasMaxLength(255);
            builder.Property(d => d.Phone).IsRequired();
        }
    }
}
