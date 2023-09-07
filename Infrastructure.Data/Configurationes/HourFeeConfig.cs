using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Configurationes
{
    public class HourFeeConfig : IEntityTypeConfiguration<HourFee>
    {
        public void Configure(EntityTypeBuilder<HourFee> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .Property(c => c.Start)
                .IsRequired();
            builder
                .Property(c => c.End)
                .IsRequired();
            builder
                .Property(c => c.FeeAmount)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        }
    }
}
