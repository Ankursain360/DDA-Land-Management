using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class GisLayerConfiguration : IEntityTypeConfiguration<Gislayer>
    {

        public void Configure(EntityTypeBuilder<Gislayer> builder)
        {
            builder.ToTable("gislayer", "lms");

            builder.HasIndex(e => e.Code)
                .HasName("Code_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FillColor)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.StrokeColor)
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}
