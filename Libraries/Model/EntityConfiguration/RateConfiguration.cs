using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {

        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            //builder.ToTable("rate", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyId).HasColumnType("int(11)");

            builder.Property(e => e.RatePercentage).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ToDate).HasColumnType("date");
        }
    }
}

