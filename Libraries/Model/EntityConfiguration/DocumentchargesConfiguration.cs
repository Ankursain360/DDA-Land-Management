using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DocumentchargesConfiguration : IEntityTypeConfiguration<Documentcharges>
    {
        public void Configure(EntityTypeBuilder<Documentcharges> builder)
        {
            builder.ToTable("documentcharges", "lms");

            builder.HasIndex(e => e.LeasePurposesTypeId)
                .HasName("fkpurposedocid_idx");

            builder.HasIndex(e => e.LeaseSubPurposeId)
                .HasName("fkleasesubdocid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentCharge).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.LeasePurposesTypeId).HasColumnType("int(11)");

            builder.Property(e => e.LeaseSubPurposeId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ToDate).HasColumnType("date");

            builder.HasOne(d => d.LeasePurposesType)
                .WithMany(p => p.Documentcharges)
                .HasForeignKey(d => d.LeasePurposesTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkpurposedocid_idx");

            builder.HasOne(d => d.LeaseSubPurpose)
                .WithMany(p => p.Documentcharges)
                .HasForeignKey(d => d.LeaseSubPurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkleasesubdocid_idx");



        }
    }
}
