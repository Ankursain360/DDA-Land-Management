using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class GroundrentConfiguration : IEntityTypeConfiguration<Groundrent>
    {
        public void Configure(EntityTypeBuilder<Groundrent> builder)
        {
            //builder.ToTable("groundrent", "lms");

            builder.HasIndex(e => e.LeasePurposesTypeId)
                .HasName("fkleasepurposegroundrateid_idx");

            builder.HasIndex(e => e.LeaseSubPurposeId)
                .HasName("fkleasesubpurposegroundrateid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.GroundRate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LeasePurposesTypeId).HasColumnType("int(11)");

            builder.Property(e => e.LeaseSubPurposeId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ToDate).HasColumnType("date");

            builder.HasOne(d => d.LeasePurposesType)
                .WithMany(p => p.Groundrent)
                .HasForeignKey(d => d.LeasePurposesTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkleasepurposegroundrateid");

            builder.HasOne(d => d.LeaseSubPurpose)
                .WithMany(p => p.Groundrent)
                .HasForeignKey(d => d.LeaseSubPurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkleasesubpurposegroundrateid");
        }
        }
    }
