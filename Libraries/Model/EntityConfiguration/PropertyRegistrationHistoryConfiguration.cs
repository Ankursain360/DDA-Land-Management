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
            entity.ToTable("propertyregistrationhistory");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CopyofOrderDocPath)
                        .HasMaxLength(500)
                        .IsUnicode(false);

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");
            entity.Property(e => e.DivisionId).HasColumnType("int(11)");
            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");
            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DateofTakenOver).HasColumnType("date");

            entity.Property(e => e.HandedOverByNameDesingnation)
                        .HasMaxLength(100)
                        .IsUnicode(false);

            entity.Property(e => e.HandedOverCommments)
                        .HasMaxLength(5000)
                        .IsUnicode(false);

            entity.Property(e => e.HandedOverDate).HasColumnType("date");

            entity.Property(e => e.HandedOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.HandedOverDivisionId).HasColumnType("int(11)");

            entity.Property(e => e.HandedOverEmailId)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            entity.Property(e => e.HandedOverFile)
                        .HasMaxLength(500)
                        .IsUnicode(false);

            entity.Property(e => e.HandedOverLandLineNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.HandedOverMobileNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.HandedOverZoneId).HasColumnType("int(11)");

            entity.Property(e => e.IsActive)
                        .HasColumnType("tinyint(4)")
                        .HasDefaultValueSql("'1'");

            entity.Property(e => e.LandTransferId).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.OrderNo)
                        .HasMaxLength(100)
                        .IsUnicode(false);

            entity.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            entity.Property(e => e.TakenOverByNameDesingnation)
                        .HasMaxLength(100)
                        .IsUnicode(false);

            entity.Property(e => e.TakenOverCommments)
                        .HasMaxLength(5000)
                        .IsUnicode(false);

            entity.Property(e => e.TakenOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.TakenOverDivisionId).HasColumnType("int(11)");

            entity.Property(e => e.TakenOverDocument)
                        .HasMaxLength(500)
                        .IsUnicode(false);

            entity.Property(e => e.TakenOverEmailId)
                        .HasMaxLength(200)
                        .IsUnicode(false);

            entity.Property(e => e.TakenOverLandLineNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.TakenOverMobileNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.TakenOverZoneId).HasColumnType("int(11)");

            entity.Property(e => e.TransferorderIssueAuthority)
                        .HasMaxLength(100)
                        .IsUnicode(false);
        }
    }
}
