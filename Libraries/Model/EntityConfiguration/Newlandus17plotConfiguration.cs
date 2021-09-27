using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class Newlandus17plotConfiguration : IEntityTypeConfiguration<Newlandus17plot>
    {
        public void Configure(EntityTypeBuilder<Newlandus17plot> builder)
        {
            //builder.ToTable("newlandus17plot", "lms");

            builder.HasIndex(e => e.KhasraId)
                     .HasName("fkus17khasra_idx");

            builder.HasIndex(e => e.NotificationId)
                .HasName("fkus17newlandnotification_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkus17village_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("int(11)");

            builder.Property(e => e.Biswa).HasColumnType("int(11)");

            builder.Property(e => e.Biswanshi).HasColumnType("int(11)");

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

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Newlandus17plot)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkus17khasra");

            builder.HasOne(d => d.Notification)
                .WithMany(p => p.Newlandus17plot)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fkus17notif");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlandus17plot)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkus17village");
        }
    }
}
