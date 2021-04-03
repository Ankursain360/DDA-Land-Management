using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class ExtensionConfiguration : IEntityTypeConfiguration<Extension>
    {
        public void Configure(EntityTypeBuilder<Extension> builder)
        {
            builder.ToTable("extension", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("fk_AllotmentIdExtintion_idx");

            builder.HasIndex(e => e.LeaseApplicationId)
                .HasName("fk_ApplicationIdExtiontion_idx");

            builder.HasIndex(e => e.ServiceTypeId)
                .HasName("fk_ServiceTypeIdExtiontion_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.ExtensionPeriod).HasColumnType("int(11)");

            builder.Property(e => e.ExtentionFees).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LeaseApplicationId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PendingAt)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ServiceTypeId).HasColumnType("int(11)");

            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.HasOne(d => d.Allotment)
                .WithMany(p => p.Extensionservice)
                .HasForeignKey(d => d.AllotmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AllotmentIdExtension");

            builder.HasOne(d => d.LeaseApplication)
                .WithMany(p => p.Extensionservice)
                .HasForeignKey(d => d.LeaseApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ApplicationIdExtension");

            builder.HasOne(d => d.ServiceType)
                .WithMany(p => p.Extensionservice)
                .HasForeignKey(d => d.ServiceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ServiceTypeIdExtension");
        }
    }
}
