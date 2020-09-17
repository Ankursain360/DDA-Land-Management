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

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Boundary).HasColumnType("int(11)");

            builder.Property(e => e.BoundaryRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.HasIndex(e => e.DepartmentId)
                  .HasName("DepartmentId");

            builder.Property(e => e.BuiltUp).HasColumnType("int(11)");

            builder.Property(e => e.BuiltUpRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ClassificationOfLandId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DisposalComments)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.DisposalDate).HasColumnType("date");

            builder.Property(e => e.DisposalTypeId).HasColumnType("int(11)");

            builder.Property(e => e.Encroached).HasColumnType("decimal(18,3)");

            builder.Property(e => e.GeoContent)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.GeoExtension)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.GeoFileName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.GeoFilePath).HasColumnType("longtext");

            builder.Property(e => e.GeoReferencing).HasColumnType("int(11)");

            builder.Property(e => e.HandedOverComments)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverDate).HasColumnType("date");

            builder.Property(e => e.HandedOverName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LandUseId).HasColumnType("int(11)");

            builder.Property(e => e.LayoutFilePath).HasColumnType("longtext");

            builder.Property(e => e.LayoutPlan).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatus).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatusRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.DeletedStatus).HasColumnType("int(11)");

            builder.Property(e => e.IsValidate).HasColumnType("int(11)");

            builder.Property(e => e.TakenOverComments)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverDate).HasColumnType("date");

            builder.Property(e => e.TakenOverName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TotalArea).HasColumnType("decimal(18,3)");

            builder.Property(e => e.UniqueId)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Vacant).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ZoneDivisionId).HasColumnType("int(11)");

            builder.HasOne(d => d.ClassificationOfLand)
                    .WithMany(p => p.Propertyregistration)
                    .HasForeignKey(d => d.ClassificationOfLandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ClassificationOfLandId");

            builder.HasOne(d => d.DisposalType)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.DisposalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DisposalTypeId");

            builder.HasOne(d => d.LandUse)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.LandUseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandUseId");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LocalityId");

            builder.HasOne(d => d.ZoneDivision)
                .WithMany(p => p.Propertyregistration)
                .HasForeignKey(d => d.ZoneDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ZoneDivisionId");
        }
    }
}

