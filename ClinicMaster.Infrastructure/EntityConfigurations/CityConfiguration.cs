using ClinicMaster.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicMaster.Infrastructure.EntityConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);

            // Seed data
            builder.HasData(
                new City { Id = 1, Name = "Minya" },
                new City { Id = 2, Name = "Beni Suef" },
                new City { Id = 3, Name = "Assiut" },
                new City { Id = 4, Name = "Cairo" },
                new City { Id = 5, Name = "Giza" },
                new City { Id = 6, Name = "Alexandria" },
                new City { Id = 7, Name = "Port Said" },
                new City { Id = 8, Name = "Suez" },
                new City { Id = 9, Name = "Ismailia" },
                new City { Id = 10, Name = "Hurghada" },
                new City { Id = 11, Name = "Luxor" },
                new City { Id = 12, Name = "Aswan" },
                new City { Id = 13, Name = "Minya" },
                new City { Id = 14, Name = "Beni Suef" },
                new City { Id = 15, Name = "Sohag" },
                new City { Id = 16, Name = "Qena" },
                new City { Id = 17, Name = "Damietta" },
                new City { Id = 18, Name = "Kafr El Sheikh" },
                new City { Id = 19, Name = "Dakahlia" },
                new City { Id = 20, Name = "Gharbia" },
                new City { Id = 21, Name = "Sharqia" },
                new City { Id = 22, Name = "Beheira" },
                new City { Id = 23, Name = "Faiyum" },
                new City { Id = 24, Name = "Ismailia" },
                new City { Id = 25, Name = "Matrouh" },
                new City { Id = 26, Name = "North Sinai" },
                new City { Id = 27, Name = "South Sinai" },
                new City { Id = 28, Name = "Red Sea" },
                new City { Id = 29, Name = "New Valley" },
                new City { Id = 30, Name = "Qalyubia" }
            );
        }
    }
}
