

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

    class NewlandnotificationdetailsConfiguration : IEntityTypeConfiguration<Newlandnotificationdetails>
    {
        public void Configure(EntityTypeBuilder<Newlandnotificationdetails> builder)
        {
            builder.ToTable("newlandnotificationdetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fk_Khasras_idx");

            builder.HasIndex(e => e.NotificationTypeId)
                .HasName("fk_notiftype_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_villages_idx");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.NotificationNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Newlandnotificationdetails)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Khasras");

            builder.HasOne(d => d.NotificationType)
                .WithMany(p => p.Newlandnotificationdetails)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_notiftype");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlandnotificationdetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_villages");



        }
    }
}