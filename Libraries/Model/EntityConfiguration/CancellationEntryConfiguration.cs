using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class CancellationEntryConfiguration : IEntityTypeConfiguration<Cancellationentry>
    {
        public void Configure(EntityTypeBuilder<Cancellationentry> builder)
        {
            builder.ToTable("cancellationentry", "lms");

            builder.HasIndex(e => e.AllotmentId)
                .HasName("fkalltmentidCancellation_idx");

            builder.HasIndex(e => e.HonebleLgOrCommon)
                .HasName("fkhonbleidCancellation_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.CancellationOrder)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseifAny)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemandLetter)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.GroundOfViolations)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.HonebleLgOrCommon).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Noc)
                .HasColumnName("NOC")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.ProceedingEvictionPossession)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Subject)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.Allotment)
                .WithMany(p => p.Cancellationentry)
                .HasForeignKey(d => d.AllotmentId)
                .HasConstraintName("fkalltmentidCancellation");

            builder.HasOne(d => d.HonebleLgOrCommonNavigation)
                .WithMany(p => p.Cancellationentry)
                .HasForeignKey(d => d.HonebleLgOrCommon)
                .HasConstraintName("fkhonbleidCancellation");
        }
    }
}
