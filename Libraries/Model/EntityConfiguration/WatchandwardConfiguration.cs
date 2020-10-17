using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

   class WatchandwardConfiguration : IEntityTypeConfiguration<Watchandward>
    {

        public void Configure(EntityTypeBuilder<Watchandward> builder)
        {


            builder.ToTable("watchandward", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkWatchWardKhasra_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkWatchWardVillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Date)
                .HasColumnName("date")
                .HasColumnType("date");

            builder.Property(e => e.Encroachment).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.Landmark)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PhotoPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ReportFiletPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.StatusOnGround)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Watchandward)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkWatchWardKhasra");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Watchandward)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fkWatchWardVillage");

        }
    }
}
