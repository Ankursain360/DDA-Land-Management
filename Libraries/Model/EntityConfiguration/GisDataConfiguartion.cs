using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class GisDataConfiguartion : IEntityTypeConfiguration<Gisdata>
    {

        public void Configure(EntityTypeBuilder<Gisdata> builder)
        {
            //builder.ToTable("gisdata", "lms");

            builder.HasIndex(e => e.GisLayerId)
                .HasName("fk_ColorCodeGisData_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_VillageIdGisData_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.GisLayerId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Label)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LabelXcoordinate)
                .HasColumnName("LabelXCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LabelYcoordinate)
                .HasColumnName("LabelYCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Polygon).HasColumnType("longtext");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.GisLayer)
                .WithMany(p => p.Gisdata)
                .HasForeignKey(d => d.GisLayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GisLayerGisData");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Gisdata)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdGisData");
        }
    }
}
