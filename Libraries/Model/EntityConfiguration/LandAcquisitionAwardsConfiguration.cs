using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class LandAcquisitionAwardsConfiguration : IEntityTypeConfiguration<LandAcquisitionAwards>
    {
        public void Configure(EntityTypeBuilder<LandAcquisitionAwards> builder)
        {
           
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.Village)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Documents).HasColumnType("longtext");

        }
    }
}
