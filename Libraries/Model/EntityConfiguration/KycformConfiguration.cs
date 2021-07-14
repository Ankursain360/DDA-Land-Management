
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

            builder.ToTable("kycform", "lms");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fklocalitykyc_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("fkzonekyc_idx");

            builder.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.AllotmentLetterDate).HasColumnType("date");

            builder.Property(e => e.AllotteeFirstName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeLastName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AllotteeMiddleName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AreaSqmt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Block)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Branch)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepositorName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Far)
                .HasColumnName("FAR")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.GroundRentPayableFromDate).HasColumnType("date");

            builder.Property(e => e.GroundRentPayableasonDate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasDefaultValueSql("1");

            builder.Property(e => e.LandPremiumPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.LeaseExecutionDate).HasColumnType("date");

            builder.Property(e => e.LeaseType)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.LicenseExecutionDate).HasColumnType("date");

            builder.Property(e => e.LicenseFeepayableAsOnDate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.LicenseFeepayableFrom).HasColumnType("date");

            builder.Property(e => e.LicenseFrom).HasColumnType("date");

            builder.Property(e => e.LicenseTo).HasColumnType("date");

            builder.Property(e => e.PayeeAadhaarNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PayeeAddress)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.PayeeEmail)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PayeeMobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.PayeeName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PayeePanno)
                .HasColumnName("PayeePANno")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PayeePincode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PayeeType)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Phase)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PhoneNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.PinCode)
                .HasMaxLength(45)
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

            builder.Property(e => e.PropertyNature)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Sector)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SecurityDepositPaid).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TenureFrom).HasColumnType("date");

            builder.Property(e => e.TenureTo).HasColumnType("date");

            builder.Property(e => e.UpfrontLicenseFeePaid).HasColumnType("decimal(18,3)");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fklocalitykyc");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Kycform)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fkzonekyc");


        }
    }
}
