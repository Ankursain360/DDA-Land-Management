

using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class KycformConfiguration : IEntityTypeConfiguration<Kycform>
    {
        public void Configure(EntityTypeBuilder<Kycform> builder)
        {
            //builder.ToTable("kycform", "lms");

            builder.HasIndex(e => e.ApprovedStatus)
                   .HasName("fk_approvalstatus_idx");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int");
            builder.HasIndex(e => e.BranchId)
                .HasName("fkbranchkyc_idx");

            builder.HasIndex(e => e.LeaseTypeId)
                .HasName("fkLeasetype_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fklocalitykyc_idx");

            builder.HasIndex(e => e.PropertyTypeId)
                .HasName("fkpropertnature_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fkzonekyc_idx");

            builder.Property(e => e.AadhaarNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.AadhaarNoPath).HasColumnType("longtext");

            builder.Property(e => e.AadhaarPanapplicantPath)
                .HasColumnName("AadhaarPANApplicantPath")
                .HasColumnType("longtext");

            builder.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AllotmentLetterDate).HasColumnType("date");

            builder.Property(e => e.AllotteeApplicantDetailsSame)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeLicenseeAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeLicenseeEmailId)
                .HasColumnName("AllotteeLicenseeEmailID")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeLicenseeMobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeLicenseeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AreaUnit)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Block)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.GroundRentAmount).HasColumnType("decimal(18,3)");
            builder.Property(e => e.LicenseFeePayable).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.KycStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.LandPremiumAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.LeaseGroundRentDepositFrequency)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LeaseLicenseExecutionDate).HasColumnType("date");

            builder.Property(e => e.LetterPath).HasColumnType("longtext");

            builder.Property(e => e.LicenseFrequency)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LicenseFrom).HasColumnType("date");

            builder.Property(e => e.LicenseTo).HasColumnType("date");

            builder.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PendingAt)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.Phase)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotDescription)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.PlotNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Pocket)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PossessionDate).HasColumnType("date");

            builder.Property(e => e.Property)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Relationship)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sector)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TenureFrom).HasColumnType("date");

            builder.Property(e => e.TenureTo).HasColumnType("date");

            builder.HasOne(d => d.Branch)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("fkbranchkyc");

            builder.HasOne(d => d.LeaseType)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.LeaseTypeId)
                .HasConstraintName("fkLeasetype");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fklocalitykyc");

            builder.HasOne(d => d.PropertyType)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.PropertyTypeId)
                .HasConstraintName("fkpropertnature");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fkzonekyc");

            builder.HasOne(d => d.ApprovedStatusNavigation)
                   .WithMany(p => p.Kycform)
                   .HasForeignKey(d => d.ApprovedStatus)
                   .HasConstraintName("fk_approvalstatus");

        }
    }
}
