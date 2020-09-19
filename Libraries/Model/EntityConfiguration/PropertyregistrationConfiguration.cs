using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class PropertyregistrationConfiguration : IEntityTypeConfiguration<Propertyregistration>
    {

        public void Configure(EntityTypeBuilder<Propertyregistration> builder)
        {
            builder.ToTable("propertyregistration", "lms");

            builder.HasIndex(e => e.ClassificationOfLandId)
                .HasName("ClassificationOfLandId");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("DepartmentId");

            builder.HasIndex(e => e.DisposalTypeId)
                .HasName("DisposalTypeId");

            builder.HasIndex(e => e.DivisionId)
                .HasName("DivisionId");

            builder.HasIndex(e => e.LocalityId)
                .HasName("LocalityId");

            builder.HasIndex(e => e.MainLandUseId)
                .HasName("MainLandUseId");

            builder.HasIndex(e => e.ZoneId)
                .HasName("registerationZoneId");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Boundary).HasColumnType("int(11)");

            builder.Property(e => e.BoundaryRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.BuiltUp).HasColumnType("int(11)");

            builder.Property(e => e.BuiltUpEncraochmentArea)
                .HasColumnType("decimal(18,3)")
                .HasDefaultValueSql("0.000");

            builder.Property(e => e.BuiltUpRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ClassificationOfLandId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DeletedBy).HasColumnType("int(11)");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DisposalComments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.DisposalDate).HasColumnType("date");

            builder.Property(e => e.DisposalTypeFilePath).HasColumnType("longtext");

            builder.Property(e => e.DisposalTypeId).HasColumnType("int(11)");

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.EncraochmentDetails)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.Encroached)
                .HasColumnType("decimal(18,3)")
                .HasDefaultValueSql("0.000");

            builder.Property(e => e.EncroachmentStatusId).HasColumnType("int(11)");

            builder.Property(e => e.GeoFilePath).HasColumnType("longtext");

            builder.Property(e => e.GeoReferencing).HasColumnType("int(11)");

            builder.Property(e => e.HandedOverComments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverDate).HasColumnType("date");

            builder.Property(e => e.HandedOverEmailId)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverFilePath).HasColumnType("longtext");

            builder.Property(e => e.HandedOverLandlineNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverMobileNo)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsDelated).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsValidate).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LayoutFilePath).HasColumnType("longtext");

            builder.Property(e => e.LayoutPlan).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatus).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatusRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.MainLandUseId)
                .HasColumnType("int(11)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Palandmark)
                .HasColumnName("PALandmark")
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.PlannedUnplannedLand)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.PrimaryListNo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.SubUse)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverComments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverDate).HasColumnType("date");

            builder.Property(e => e.TakenOverEmailId)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverFilePath).HasColumnType("longtext");

            builder.Property(e => e.TakenOverLandlineNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverMobileNo)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TotalArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalAreaInBigha)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Vacant)
                .HasColumnType("decimal(18,3)")
                .HasDefaultValueSql("0.000");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.ClassificationOfLand)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.ClassificationOfLandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClassificationOfLandId");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DepartmentId");

            builder.HasOne(d => d.DisposalType)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.DisposalTypeId)
                .HasConstraintName("DisposalTypeId");

            builder.HasOne(d => d.Division)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DivisionId");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LocalityId");

            builder.HasOne(d => d.MainLandUse)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.MainLandUseId)
                .HasConstraintName("MainLandUseId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registerationZoneId");
        }
    }
}

