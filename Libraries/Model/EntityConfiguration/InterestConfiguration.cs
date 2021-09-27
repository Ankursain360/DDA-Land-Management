using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class InterestConfiguration : IEntityTypeConfiguration<Interest>     
    {

        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            //builder.ToTable("interest", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Percentage)
                .HasColumnName("percentage")
                .HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyId).HasColumnType("int(11)");

            builder.Property(e => e.ToDate).HasColumnType("date");
        }
    }
}

