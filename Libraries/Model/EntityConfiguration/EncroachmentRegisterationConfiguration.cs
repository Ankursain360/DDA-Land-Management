using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class EncroachmentRegisterationConfiguration: IEntityTypeConfiguration<EncroachmentRegisteration>
    {
        public void Configure(EntityTypeBuilder<EncroachmentRegisteration> builder)
        {
                builder.ToTable("encroachmentregisteration");

            builder.HasIndex(e => e.ApprovedStatus)
                .HasName("fk_ApprovedStatusEncroachmentregistration_idx");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("EncroachmentDeptId_idx");

            builder.HasIndex(e => e.DivisionId)
                .HasName("EncroachmentDivisionId_idx");

            builder.HasIndex(e => e.WatchWardId)
                    .HasName("fk_watchwardid_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("EncroachmentLocality_idx");

            builder.HasIndex(e => e.OtherDepartment)
                .HasName("EncroachmentDeptId_idx1");

            builder.HasIndex(e => e.ZoneId)
                .HasName("EncroachmentZonetId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApprovalZoneId).HasColumnType("int(11)");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.AreaUnit).HasColumnType("int(11)");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.EncrochmentDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsPossession)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsEncroachment)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");
            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OtherDepartment).HasColumnType("int(11)");

            builder.Property(e => e.PendingAt)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PoliceStation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PossessionType)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.RefNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LocationAddressWithLandMark)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.EncroacherName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.SecurityGuardOnDuty)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.StatusOfLand)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TotalAreaInSqAcreHt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.ApprovedStatusNavigation)
                .WithMany(p => p.EncroachmentRegisteration)
                .HasForeignKey(d => d.ApprovedStatus)
                .HasConstraintName("fk_ApprovedStatusEncroachmentregistration");


            builder.HasOne(d => d.Department)
                .WithMany(p => p.EncroachmentregisterationDepartment)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentDeptId");

            builder.HasOne(d => d.Division)
                .WithMany(p => p.EncroachmentRegisteration)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentDivisionId");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.EncroachmentRegisteration)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentLocality");

            builder.HasOne(d => d.OtherDepartmentNavigation)
                .WithMany(p => p.EncroachmentregisterationOtherDepartmentNavigation)
                .HasForeignKey(d => d.OtherDepartment)
                .HasConstraintName("EncroachmentOtherDeptId");

            builder.HasOne(d => d.WatchWard)
                .WithMany(p => p.EncroachmentRegisteration)
                .HasForeignKey(d => d.WatchWardId)
                .HasConstraintName("fk_watchwardid");


            builder.HasOne(d => d.Zone)
                .WithMany(p => p.EncroachmentRegisteration)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentZonetId");
        }
    }
}