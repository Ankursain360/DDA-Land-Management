using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class GisVillageTextConfiguration : IEntityTypeConfiguration<Gisvillagetext>
    {
        public void Configure(EntityTypeBuilder<Gisvillagetext> builder)
        {
            builder.ToTable("gisvillagetext", "lms");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_VillageIdVillageText_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Label)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Polygon).HasColumnType("longtext");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasColumnType("decimal(12,8)");

            builder.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasColumnType("decimal(12,8)");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Gisvillagetext)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdVillageText");
        }
    }
}
