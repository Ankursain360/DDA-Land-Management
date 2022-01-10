using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class RestorationentryConfiguration : IEntityTypeConfiguration<Restorationentry>
    {
        public void Configure(EntityTypeBuilder<Restorationentry> builder)
        {
            builder.HasIndex(e => e.AllotmentId)
                  .HasName("fkrestorationallotmentid_idx");

            builder.HasIndex(e => e.Cancellationid)
                .HasName("fkcancellationid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

            builder.Property(e => e.Cancellationid)
                .HasColumnName("cancellationid")
                .HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.RestorationDate).HasColumnType("date");

            builder.Property(e => e.RestorationOrder)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.RestorationRemarks)
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.HasOne(d => d.Allotment)
                .WithMany(p => p.Restorationentry)
                .HasForeignKey(d => d.AllotmentId)
                .HasConstraintName("fkrestorationallotmentid");

            builder.HasOne(d => d.Cancellation)
                .WithMany(p => p.Restorationentry)
                .HasForeignKey(d => d.Cancellationid)
                .HasConstraintName("fkcancellationid");
        }

    }
}
