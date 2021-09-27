using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class PropertyRegistrationHistoryConfiguration : IEntityTypeConfiguration<PropertyRegistrationHistory>
    {
        public void Configure(EntityTypeBuilder<PropertyRegistrationHistory> entity)
        {
            //entity.ToTable("propertyregistrationhistory");

            entity.HasIndex(e => e.DepartmentId)
                .HasName("LandTransferHistoryCurrentDepartment_idx");

            entity.HasIndex(e => e.DivisionId)
                .HasName("LandTransferHistoryCurrentDivsion_idx");

            entity.HasIndex(e => e.LandTransferId)
                .HasName("LandTransferHistoryLandTransferId_idx");

            entity.HasIndex(e => e.PropertyRegistrationId)
                .HasName("LandTransferHistoryPropertyRegistrationId_idx");

            entity.HasIndex(e => e.ZoneId)
                .HasName("LandTransferHistoryCurrentZone_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.DivisionId).HasColumnType("int(11)");

            entity.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.LandTransferId).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Propertyregistrationhistory)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHistoryCurrentDepartmentId");

            entity.HasOne(d => d.Division)
                .WithMany(p => p.Propertyregistrationhistory)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHistoryCurrentDivsionId");

            entity.HasOne(d => d.LandTransfer)
                .WithMany(p => p.Propertyregistrationhistory)
                .HasForeignKey(d => d.LandTransferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHistoryLandTransferId");

            entity.HasOne(d => d.PropertyRegistration)
                .WithMany(p => p.Propertyregistrationhistory)
                .HasForeignKey(d => d.PropertyRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHistoryPropertyRegistrationId");

            entity.HasOne(d => d.Zone)
                .WithMany(p => p.Propertyregistrationhistory)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHistoryCurrentZoneId");
        }
    }
}
