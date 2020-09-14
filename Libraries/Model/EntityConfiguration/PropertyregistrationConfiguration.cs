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

            builder.Property(e => e.BuiltUp).HasColumnType("int(11)");

            builder.Property(e => e.BuiltUpRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.ClassificationOfLand).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DisposalComments)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.DisposalDate).HasColumnType("date");

            builder.Property(e => e.DisposalType).HasColumnType("int(11)");

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

            builder.Property(e => e.LandUse).HasColumnType("int(11)");

            builder.Property(e => e.LayoutContent)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LayoutExtension)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LayoutFileName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LayoutFilePath).HasColumnType("longtext");

            builder.Property(e => e.LayoutPlan).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatus).HasColumnType("int(11)");

            builder.Property(e => e.LitigationStatusRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.Locality).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

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

            builder.Property(e => e.ZoneDivision).HasColumnType("int(11)");
        }
    }
}

