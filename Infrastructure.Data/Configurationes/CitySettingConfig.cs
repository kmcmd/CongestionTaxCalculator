using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Infrastructure.Data.Configurationes
{
    public class CitySettingConfig : IEntityTypeConfiguration<CitySetting>
    {
        public void Configure(EntityTypeBuilder<CitySetting> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(200);
            builder.Property(c => c.MinimumTimeCalcuteFee).IsRequired();
            builder.Property(c => c.MaximunFeePerDay).IsRequired();

            builder.Property(c => c.FreeMonthList)
                .HasConversion(
                    v=> JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v=> v != null ? JsonSerializer.Deserialize<List<int>>(v, new JsonSerializerOptions()) : null
                );
        }
    }
}
