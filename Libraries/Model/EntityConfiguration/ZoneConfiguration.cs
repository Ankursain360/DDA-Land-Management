﻿using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {

        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            //builder.ToTable("zone", "lms");

            builder.HasIndex(e => e.Code)
                .HasName("Code_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fkzonedepartment_idx");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            builder.Property(e => e.Polygon).HasColumnType("longtext");
            builder.Property(e => e.Xcoordinate)
                .HasColumnName("XCoordinate")
                .HasColumnType("decimal(12,8)");

            builder.Property(e => e.Ycoordinate)
                .HasColumnName("YCoordinate")
                .HasColumnType("decimal(12,8)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Zone)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkzonedepartment");
        }
    }
}
