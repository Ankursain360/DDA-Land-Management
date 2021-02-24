using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class Newlandus22plotConfiguration : IEntityTypeConfiguration<Newlandus22plot>
    {
        public void Configure(EntityTypeBuilder<Newlandus22plot> builder)
        {
            builder.ToTable("newlandus22plot", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("FK22khasraId_idx");

            builder.HasIndex(e => e.NotificationId)
                .HasName("FK22notifId_idx");

            builder.HasIndex(e => e.Us17Id)
                .HasName("FKus17Id_idx");

            builder.HasIndex(e => e.Us4Id)
                .HasName("FKus4Id_idx");

            builder.HasIndex(e => e.Us6Id)
                .HasName("FKus6Id_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("FK22vilId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NotificationId).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Us17Id).HasColumnType("int(11)");

            builder.Property(e => e.Us4Id).HasColumnType("int(11)");

            builder.Property(e => e.Us6Id).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk22uskhasra");

            builder.HasOne(d => d.Notification)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkus22notifid");

            builder.HasOne(d => d.Us17)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.Us17Id)
                .HasConstraintName("fkusplot17usid");

            builder.HasOne(d => d.Us4)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.Us4Id)
                .HasConstraintName("fkplot4usid");

            builder.HasOne(d => d.Us6)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.Us6Id)
                .HasConstraintName("fkplot6usid");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlandus22plot)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk22usvilage");

        }
    } 
}
